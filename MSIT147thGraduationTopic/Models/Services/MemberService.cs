﻿using Microsoft.EntityFrameworkCore;
using MSIT147thGraduationTopic.EFModels;
using MSIT147thGraduationTopic.Models.Dtos;
using MSIT147thGraduationTopic.Models.Infra.ExtendMethods;
using MSIT147thGraduationTopic.Models.Infra.Repositories;
using MSIT147thGraduationTopic.Models.Infra.Utility;
using MSIT147thGraduationTopic.Models.ViewModels;
using System;

namespace MSIT147thGraduationTopic.Models.Services
{
    public class MemberService
    {
        private readonly GraduationTopicContext _context;
        private readonly MemberRepository _repo;
        private readonly IWebHostEnvironment _environment;

        public MemberService(GraduationTopicContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _repo = new MemberRepository(context);
        }

        public IEnumerable<MemberVM> GetAllMembers()
        {
            return _repo.GetAllMembers().Select(dto =>
            {
                string htmlFilePath = Path.Combine(_environment.WebRootPath, @"uploads\Avatar");

                return dto.ToVM();
            });
        }

        public int CreateMember(MemberDto dto, IFormFile file)
        {
            if (file != null)
            {
                string path = Path.Combine(_environment.WebRootPath, @"uploads\Avatar", file.FileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                dto.Avatar = file.FileName;
            }

            var salt = new RandomGenerator().RandomSalt();
            dto.Salt = salt;
            dto.Password = dto.Password?.GetSaltedSha256(salt);           

            return _repo.CreateMember(dto);
        }

        public int EditMember(MemberEditDto dto, int memberId, IFormFile file)
        {
            if (file != null)
            {
                string path = Path.Combine(_environment.WebRootPath, @"uploads\Avatar", file.FileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            string? fileName = file?.FileName;
            return _repo.EditMember(dto, memberId, fileName);
        }

        public int DeleteMember(int memberId)
        {
            return _repo.DeleteMember(memberId);
        }
    }

    public class AddressService
    {

    }
}
