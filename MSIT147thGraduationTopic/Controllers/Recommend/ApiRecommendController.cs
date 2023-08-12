﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIT147thGraduationTopic.EFModels;
using MSIT147thGraduationTopic.Models.Dtos.Recommend;
using MSIT147thGraduationTopic.Models.Services;
using System.ComponentModel.DataAnnotations;

namespace MSIT147thGraduationTopic.Controllers.Recommend
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRecommendController : ControllerBase
    {
        private readonly GraduationTopicContext _context;
        private readonly RecommendService _service;

        public ApiRecommendController(GraduationTopicContext context)
        {
            _context = context;
            _service = new(context);
        }


        [HttpGet("refreshpopularity")]
        public async Task<ActionResult<int>> RefreshPopularity()
        {
            return await _service.CalculatePopularities();
        }


        public record RateDataRecord(int Num, string Data);
        [HttpPut("ratedata")]
        public async Task<ActionResult<int>> UpdateRateEvaluationFunc(RateDataRecord record)
        {
            string col = record.Data.ToLower() switch
            {
                "evaluationweight" => "[EvaluationWeight]",
                "purchasedeight" => "[PurchasedWeight]",
                "manuallyweight" => "[ManuallyWeight]",
                "rateevaluationfunc" => "[RateEvaluationFunc]",
                "ratepurchasefunc" => "[RatePurchaseFunc]",
                _ => "",
            };
            if (string.IsNullOrEmpty(col) || record.Num < 0) return -1;
            return await _service.UpdateRatingData(record.Num, col);
        }



        [HttpGet("mostpopularspecs")]
        public async Task<ActionResult<List<string>>> GetMostPopularSpecsName(int top = 10)
        {
            return await _service.GetMostPopularSpecsName(top);
        }


        [HttpGet("getsearcheditems")]
        public async Task<ActionResult<IEnumerable<SearchedItemsDto>>> GetSearchedItems(string text, string type)
        {
            return await _service.GetSearchedItems(text, type);
        }


        public record InsertEntriesRecord(int[] Ids, [Range(-10, 10)] int Weight, string Type);
        [HttpPost("insertweightentries")]
        public async Task<ActionResult<int>> InsertWeightedEntries(InsertEntriesRecord record)
        {
            if (record == null) return -1;
            return await _service.InsertWeightedEntries(record);
        }


        [HttpGet("getallweightedentries")]
        public async Task<ActionResult<List<WeightedEntryDisplayDto>>> GetAllWeightedEntries()
        {
            return await _service.GetAllWeightedEntries();
        }

        public record UpdateEntryWeightRecord(int Id, [Range(-10, 10)] int Weight);
        [HttpPut("updateentryweight")]
        public async Task<ActionResult<int>> UpdateEntryWeight(UpdateEntryWeightRecord record)
        {
            return await _service.UpdateEntryWeight(record.Id, record.Weight);
        }

        [HttpDelete("deleteweightentry/{id}")]
        public async Task<ActionResult<int>> DeleteWeightedEntry(int id)
        {
            return await _service.DeleteWeightedEntry(id);
        }

    }
}
