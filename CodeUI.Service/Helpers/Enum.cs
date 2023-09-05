using System.ComponentModel.DataAnnotations;

namespace CodeUI.Service.Helpers
{
    public class Enum

    {
        public enum FcmTokenType
        {
            Customer = 1,
            Staff = 2
        }

        public enum RedisSetUpType
        {
            GET = 1,
            SET = 2,
            DELETE = 3
        }
        public enum SystemRoleTypeEnum
        {
            [Display(Name = "Những con cáo")]
            SystemAdmin = 1,
            [Display(Name = "Quản lý cửa hàng")]
            StoreManager = 2,
            [Display(Name = "Người giao hàng")]
            Shipper = 3
        }
        public enum AccountTypeEnum
        {
            CreditAccount = 1,
            PointAccount = 2
        }
    }
}