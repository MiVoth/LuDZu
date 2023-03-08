namespace LmmPlanner.Data.Helper
{
    public static class ExceptionHelper
    {
        public static string TypeToString(long? type)
        {
            string typeName = string.Empty;
            if (type == null)
            {
                return typeName;
            }
            switch (type)
            {
                case 1:
                    typeName = "Regionaler Kongress";
                    break;
                case 2:
                    typeName = "Gedächtnismahl";
                    break;
                case 0:
                    typeName = "Dienstwoche?";
                    break;
                case 5:
                    typeName = "Kreiskongress";
                    break;
                default:
                    typeName = $"Unbekannt";
                    break;
            }
            return typeName;
        }
    }
}