using System.ComponentModel.DataAnnotations;

namespace WebAppMVCLesson1.Models
{
    public class Users
    {
        [Key] //первичный ключ по дефолту находится по шаблону - Имя класса + Id, key позволяет обозначить первчный ключ
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
