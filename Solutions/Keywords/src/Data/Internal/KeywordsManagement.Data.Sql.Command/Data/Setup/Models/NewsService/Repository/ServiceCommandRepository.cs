﻿namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.NewsService.Contracts;
using Sql.Commands;
using Service = Core.NewsService.Models.NewsService;

public class ServiceCommandRepository(KeywordsManagementCommandContext context) :
    CommandRepository<KeywordsManagementCommandContext, Service>(context), INewsServiceCommandRepository
{ }