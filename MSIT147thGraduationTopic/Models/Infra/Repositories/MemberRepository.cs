﻿using Microsoft.EntityFrameworkCore;
using MSIT147thGraduationTopic.EFModels;
using MSIT147thGraduationTopic.Models.Dtos;
using System.Linq;

namespace MSIT147thGraduationTopic.Models.Infra.Repositories
{
    public class MemberRepository
    {
        private readonly GraduationTopicContext _context;

        public MemberRepository(GraduationTopicContext context)
        {
            _context = context;
        }
        public IEnumerable<MemberDto> GetAllMembers()
        {
            var members = _context.Members.ToList();
            return members.Select(o => o.ToDto());
        }

        public IEnumerable<MemberDto> GetMemberByNameOrAccount(string query)
        {
            var members = _context.Members
                .Where(o => o.MemberName.Contains(query) || o.Account.Contains(query));
            return members.Select(o => o.ToDto());
        }

        public IEnumerable<MemberDto> GetMemberById(int memberId)
        {
            var members = _context.Members
                .Where(o => o.MemberId == memberId);
            return members.Select(o => o.ToDto());
        }

        public int CreateMember(MemberDto dto)
        {
            var db = _context;
            var obj = dto.ToEF();
            db.Members.Add(obj);
            db.SaveChanges();
            return obj.MemberId;
        }

        public int EditMember(MemberEditDto dto, int memberId, string fileName)
        {
            var member = _context.Members.FirstOrDefault(o => o.MemberId == memberId);
            if (member == null) return -1;

            member.ChangeByEditDto(dto);
            member.Avatar = fileName;

            _context.SaveChanges();
            return memberId;
        }

        public int DeleteMember(int memberId)
        {
            var member = _context.Members.Find(memberId);
            if (member == null) return -1;

            _context.Members.Remove(member);

            _context.SaveChanges();
            return memberId;
        }
    }
}
