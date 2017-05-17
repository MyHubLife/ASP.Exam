using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Вставьте первый адрес доставки")]
        //public string Line1 { get; set; }
        //public string Line2 { get; set; }
        //public string Line3 { get; set; }
        //[Required(ErrorMessage = "Укажите город")]
        //public string City { get; set; }
        //[Required(ErrorMessage = "Укажите страну")]
        //public string Country { get; set; }

        [Required(ErrorMessage = "Вставьте первый адрес доставки")]
        [Display(Name = "Первый адрес")]
        public string Line1 { get; set; }

        [Display(Name = "Второй адрес")]
        public string Line2 { get; set; }

        [Display(Name = "Третий адрес")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Укажите Ваш контактній телефон")]
        [Display(Name = "Ваш контактный телефон")]
        public string Telephone { get; set; } //City

        [Required(ErrorMessage = "Укажите Ваш адрес электронной почты")]
        [Display(Name = "Ваш e-mail")]
        public string Email { get; set; } //Country

        public bool GiftWrap { get; set; }
    }
}
