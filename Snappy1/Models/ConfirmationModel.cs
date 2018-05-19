namespace Snappy1.Models
{
    public class ConfirmationModel
    {
        public ConfirmationModel()
        {
            UserModel = new UserInfoModel();
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsBookingConfirmed { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public string BookingNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserInfoModel UserModel { get; set; }
    }
}