namespace SmartNeighborhoodAPI.Entites.Enums
{
    public enum IdentityType
    {
        [Display(Name = "بطاقة شخصية")]
        IdentityCard = 1,
        [Display(Name = "جواز سفر")]
        Passport,
        [Display(Name = "شهادة ميلاد")]
        BirthCertificate
    }
}
