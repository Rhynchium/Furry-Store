﻿using Microsoft.EntityFrameworkCore;
using MSIT147thGraduationTopic.EFModels;
using MSIT147thGraduationTopic.Models.Dtos;

namespace MSIT147thGraduationTopic.Models.Infra.Repositories
{
    public class CouponRepository
    {
        private readonly GraduationTopicContext _context;
        public CouponRepository(GraduationTopicContext context)
        {
            _context = context;
        }

        public IEnumerable<CouponDto> GetAllCoupons()
        {
            var coupons = _context.Coupons.ToList();
            return coupons.Select(c => c.ToDto());
        }

        public IEnumerable<CouponDto> ShowCoupons(int id)
        {
            var coupons = _context.Coupons.Where(c => c.CouponDiscountTypeId == id);

            return coupons.ToList().Select(c => c.ToDto());
        }

        public IEnumerable<CouponFrontDto> ShowCouponsFront(int id)
        {
            var coupons = _context.CouponReceives.Where(c=>c.MemberId == id);
            return coupons.ToList().Select(c=>c.ToFrontDto());
        }

        public int CreateCoupon(CouponDto cDto)
        {
            var obj = cDto.ToEF();
            _context.Coupons.Add(obj);
            _context.SaveChanges();
            return obj.CouponId;
        }

        public int EditCoupon(CouponEditDto cEDto)
        {
            var couponData = _context.Coupons.FirstOrDefault(c=>c.CouponId == cEDto.CouponId);

            if(couponData == null)
            {
                return -1;
            }
            //couponData.ChangeByDto(ceDto)

            couponData.CouponName = cEDto.CouponName;
            couponData.CouponTagId = cEDto.CouponTagId;
            couponData.CouponStartDate = cEDto.CouponStartDate;
            couponData.CouponEndDate = cEDto.CouponEndDate;
            couponData.CouponDiscount= cEDto.CouponDiscount;
            couponData.CouponCondition= cEDto.CouponCondition;

            _context.Coupons.Update(couponData);
            _context.SaveChanges();
            return couponData.CouponId;
        }

        public int DeleteCoupon(int couponId)
        {
            var coupon = _context.Coupons.Find(couponId);
            if(coupon == null)
            {
                return -1;
            }
            
            _context.Coupons.Remove(coupon);

            _context.SaveChanges();
            return couponId;
        }

        public CouponDto GetCouponById(int id)
        {
            // 通過id搜尋單筆資料
            var coupon = _context.Coupons.FirstOrDefault(c => c.CouponId == id);
            if (coupon == null)
            {
                // 未搜尋到id時的處理
                return null;
            }

            return coupon.ToDto(); //將coupon實體轉換為couponDto
        }

        public CouponFrontDto GetCouponByMemberID(int id)
        {
            var coupon = _context.CouponReceives.FirstOrDefault(c=>c.MemberId == id);
            if(coupon == null)
            {
                return null;
            }
            return coupon.ToFrontDto();
        }


    }
}
