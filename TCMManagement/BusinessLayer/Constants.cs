namespace TCMManagement.BusinessLayer
{
    public static class Constants
    {
        public enum Include
        {
            None = 0,
            Role,
            Person,
            Patient,
            Appointment,
            MedicalRecord,
            Treatment
        }

        public enum PaymentMethod
        {
            Cash = 0,
            Debit,
            Credit,
            ApplePay,
            WeChat
        }
    }
}