﻿// See https://aka.ms/new-console-template for more information

using MSIT147thGraduationTopic.Models.Services;

var service = new RandomInsertService(null);

service.AddRandomMembers(20);
//service.AddRandomCart();

Console.WriteLine("success");

