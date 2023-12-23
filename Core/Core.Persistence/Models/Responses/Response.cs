using System.Reflection;

namespace Core.Persistence.Models.Responses
{
    public interface IResponseBase
    {
        int StatusCode { get; set; }

        bool IsSuccessful { get; set; }

        List<string> Errors { get; set; }

        IResponseBase ToTypedResponse(IResponseBase source);

    }

    /// <summary>
    /// Api den dönen veriler Response ile dönülmesi zorunludur.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : IResponseBase
    {
        public T Data { get; set; }

        public int StatusCode { get; set; }

        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        // Static Factory Method
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Success(string message, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { message }, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(T data, string error, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = false, Errors = new List<string> { error } };
        }

        public static Response<T> Fail(T data, List<string> errors, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = false, Errors = errors };
        }

        public Response<TNew> FailConvert<TNew>()
            where TNew : class
        {
            return new Response<TNew> { Errors = Errors, StatusCode = StatusCode, IsSuccessful = IsSuccessful, Data = null };
        }

        public IResponseBase ToTypedResponse(IResponseBase source)
        {
            Errors = source.Errors;
            IsSuccessful = source.IsSuccessful;
            StatusCode = source.StatusCode;

            var val = source.GetType().GetProperty(nameof(DynamicResponse.Data))?.GetValue(source, null);
            if (val != null)
                Data = (T)val;

            return this;
        }
    }

    public class DynamicResponse : Response<dynamic>
    {
        /// <summary>
        /// object olarak gelen bir verinin propertylerini ResponseBase e eşitleyip instance döner.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DynamicResponse ToDynamicResponse(object obj)
        {
            var res = new DynamicResponse();
            Type sourceType = obj.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] targetProperties = typeof(DynamicResponse).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var sourceProp in sourceProperties)
            {
                var selectedProp = targetProperties.FirstOrDefault(w => w.Name == sourceProp.Name);
                if (selectedProp != null)
                {
                    selectedProp.SetValue(res, sourceProp.GetValue(obj, null));
                }
            }
            return res;
        }

    }

}
