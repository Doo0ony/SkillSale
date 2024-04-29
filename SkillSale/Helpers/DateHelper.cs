namespace SkillSale.Helpers
{
    public static class DateHelper
    {
        public static int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;

            if (DateTime.Today < birthDate.AddYears(age))
                age--;

            return age;
        }
    }
}
