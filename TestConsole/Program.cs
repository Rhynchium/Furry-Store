﻿// See https://aka.ms/new-console-template for more information

using MSIT147thGraduationTopic.Models.Services;
using System.Security.Cryptography;

var service = new RandomInsertService(null);

//RandomNumberGenerator.GetInt32(10)

//service.AddRandomMembers(50);
//service.AddRandomMerchandiseAndSpecs(90);

//service.AddRandomCart();
//service.AddRandomOrders();
//service.AddSpecTags();
//service.AddSpecPopularity();
service.AddRandomEvaluations();



Console.WriteLine("success");

