namespace Core.Persistence.Models.Responses
{
    /// <summary>
    /// Any sorgularını bu model ile dönüyoruz
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AnyResponseDetail<T>
    {
        /// <summary>
        /// Hangi verinin varlıgını kontrol ediyoruz
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Var mı bilgisi
        /// </summary>
        public bool IsAny { get; set; }
    }
}
