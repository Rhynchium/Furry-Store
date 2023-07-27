﻿using Humanizer;
using MSIT147thGraduationTopic.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MSIT147thGraduationTopic.Models.ViewModels
{
    public class MemberVM
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string NickName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Salt { get; set; }
        public bool IsActivated { get; set; }
        public string ConfirmGuid { get; set; }
    }

    public class MemberCreateVM
    {
        public int MemberId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(30, ErrorMessage = "{0}長度不可多於{1}")]
        public string? MemberName { get; set; }

        [MaxLength(30, ErrorMessage = "{0}長度不可多於{1}")]
        public string? NickName { get; set; }

        [Required]
        [DateTimeRange(-100, -18, ErrorMessage = "年齡不可大於100歲,小於18歲!")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(15, ErrorMessage = "{0}長度不可多於{1}")]
        public string? Account { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(20, ErrorMessage = "{0}長度不可多於{1}")]
        public string? Phone { get; set; }

        [MaxLength(30, ErrorMessage = "{0}長度不可多於{1}")]
        public string? Address { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [MaxLength(30, ErrorMessage = "{0}長度不可多於{1}")]
        public string? Email { get; set; }
        public string? Avatar { get; set; }

        [Required]
        public string? Salt { get; set; }
        [Required]
        public bool IsActivated { get; set; }
        [Required]
        public string? ConfirmGuid { get; set; }
    }

    public class MemberEditVM
    {
        public int MemberId { get; set; }
        [MaxLength(30, ErrorMessage = "{0}長度不可多於{1}")]
        public string? NickName { get; set; }
        public string? Password { get; set; }

        [MaxLength(20, ErrorMessage = "{0}長度不可多於{1}")]
        public string? Phone { get; set; }

        [MaxLength(30, ErrorMessage = "{0}長度不可多於{1}")]
        public string? Address { get; set; }

        [EmailAddress]
        [MaxLength(30, ErrorMessage = "{0}長度不可多於{1}")]
        public string? Email { get; set; }
        public string? Avatar { get; set; }
    }

    public class AddressVM
    {
        public class Rootobject
        {
            public Result result { get; set; }
            public bool success { get; set; }
        }

        public class Result
        {
            public string resource_id { get; set; }
            public int limit { get; set; }
            public int total { get; set; }
            public Field[] fields { get; set; }
            public Record[] records { get; set; }
        }

        public class Field
        {
            public string type { get; set; }
            public string id { get; set; }
        }

        public class Record
        {
            public string city { get; set; }
            public string site_id { get; set; }
            public string road { get; set; }
        }

    }



    public class DateTimeRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;
        private readonly DateTime _maxDate;

        public DateTimeRangeAttribute(int initialyear, int finalyear)
        {
            _minDate = DateTime.Now.AddYears(initialyear);
            _maxDate = DateTime.Now.AddYears(finalyear);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue < _minDate)
                {
                    return new ValidationResult($"生日不可早於 {_minDate:yyyy-MM-dd}",
                        new[] { validationContext.MemberName });
                }
                else if (dateValue > _maxDate)
                {
                    return new ValidationResult($"生日不可晚於 {_maxDate:yyyy-MM-dd}",
                        new[] { validationContext.MemberName });
                }
            }
            return ValidationResult.Success;
        }
    }

    public static class MemberVMTransfer
    {
        public static MemberVM ToVM(this MemberDto dto)
        {
            return new MemberVM
            {
                MemberId = dto.MemberId,
                MemberName = dto.MemberName,
                NickName = dto.NickName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Account = dto.Account,
                Phone = dto.Phone,
                Address = dto.Address,
                Email = dto.Email,
                Salt = dto.Salt,
                IsActivated = dto.IsActivated,
                ConfirmGuid = dto.ConfirmGuid,
            };
        }

        public static MemberDto ToDto(this MemberCreateVM vm)
        {
            return new MemberDto
            {
                MemberId = vm.MemberId,
                MemberName = vm.MemberName,
                NickName = vm.NickName,
                DateOfBirth = vm.DateOfBirth,
                Account = vm.Account,
                Password = vm.Password,
                Phone = vm.Phone,
                Address = vm.Address,
                Email = vm.Email,
                Salt = vm.Salt,
                IsActivated = vm.IsActivated,
                ConfirmGuid = vm.ConfirmGuid,
            };
        }

        public static MemberEditVM ToEditVM(this MemberDto dto)
        {
            return new MemberEditVM
            {
                MemberId = dto.MemberId,
                NickName = dto.NickName,
                Password = dto.Password,
                Phone = dto.Phone,
                Address = dto.Address,
                Email = dto.Email,
                Avatar = dto.Avatar,
            };
        }
    }
}