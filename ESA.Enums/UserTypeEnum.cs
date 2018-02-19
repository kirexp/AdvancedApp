using System.ComponentModel.DataAnnotations;

namespace Enums {
    public enum UserTypeEnum {
        [Display(Name = "�����")]
        None = 0,
        [Display(Name = "��������")]
        Employee = 1,
        [Display(Name = "������")]
        Client = 2,
        [Display(Name = "�����")]
        Admin = 3
    }
}