namespace RSF
{
    public static class Settings
    {
        private static int accuracy;
        private static bool jsonSaving;

        public static int Accuracy
        {
            get
            {
                return accuracy;
            }

            set
            {
                accuracy = value;
            }
        }

        public static bool JsonSaving
        {
            get
            {
                return jsonSaving;
            }

            set
            {
                jsonSaving = value;
            }
        }
    }
}
