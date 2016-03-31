namespace Issue320
{
    public class LoginRequestDTO
    {
        public string SomeId { get; set; }
    }


    public class LoginResponseDTO
    {
        public string UserToken { get; set; }

        public int ValidDuration { get; set; }
    }
}
