namespace PashaInsuranceTest.Helpers
{
    public static class ErrorMessage
    {
        public static class AttributeError {
            public const string INCORRECT_CHOISE            = "Selected value is not allowed";
            public const string INVALID_PRIMARY_KEY         = "Invalid primary key";
        }

        public static class DbLookup {
            public const string DOES_NOT_EXIST_FOR_ID       = "{0} by id '{1}' does not exists";
            public const string EXISTS_WITH_NAME            = "{0} with name '{1}' exists";

        }

    }
}
