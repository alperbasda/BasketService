
namespace Core.CrossCuttingConcerns.Helpers.IntegerHelpers
{
    public static class IntegerHelper
    {


        private static readonly string[] birler = { "", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz" };
        private static readonly string[] onlar = { "", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
        private static readonly string[] binler = { "", "bin", "milyon", "milyar", "trilyon", "katrilyon" }; // Daha büyük değerler için genişletilebilir.



        /// <summary>
        /// Verilen uzunlukta rasgele integer sayı üretir.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int CreateNumber(int length)
        {
            int[] list = new int[length];
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
                list[i] = rnd.Next(0, 9);

            return Convert.ToInt32(string.Join("", list));
        }

        /// <summary>
        /// int sayiyi türkçe olarak döndürür
        /// </summary>
        /// <param name="sayi"></param>
        /// <returns></returns>
        public static string ToTrString(this int sayi)
        {
            if (sayi == 0)
                return "sıfır";

            string turkceYazi = "";
            int grupIndex = 0;

            while (sayi > 0)
            {
                int grupDegeri = (int)(sayi % 1000);
                if (grupDegeri > 0)
                {
                    turkceYazi = $"{GrupDegeriniTurkceYaziyaCevir(grupDegeri)} {binler[grupIndex]} {turkceYazi}";
                }

                sayi /= 1000;
                grupIndex++;
            }

            return turkceYazi.Trim();
        }

        private static string GrupDegeriniTurkceYaziyaCevir(int grupDegeri)
        {
            string turkceYazi = "";

            int yuzlern = grupDegeri / 100;
            int onlarn = (grupDegeri % 100) / 10;
            int birlern = grupDegeri % 10;

            if (yuzlern > 0)
                turkceYazi += $"{birler[yuzlern]} yüz ";

            if (onlarn > 0)
                turkceYazi += $"{onlar[onlarn]} ";

            if (birlern > 0)
                turkceYazi += $"{birler[birlern]} ";

            return turkceYazi;
        }

    }
}
