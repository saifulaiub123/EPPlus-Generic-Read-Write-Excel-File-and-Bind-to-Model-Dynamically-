using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using DataExchangeWorkerService.Configuration;
using DataExchangeWorkerService.Helpers;
using DataExchangeWorkerService.Models;
using Microsoft.Extensions.Logging;

namespace DataExchangeWorkerService.Services
{
    public class EtlProcessor
    {
        private readonly ILogger<EtlProcessor> _logger;
        private readonly WorkerOptions _options;
        private readonly IMapper _mapper;
        private readonly string _sourceFilePath;
        private readonly string _destinationFilePath;
        private readonly string _outputFilePath;
        private readonly string _templateFilePath;


        public EtlProcessor(ILogger<EtlProcessor> logger, WorkerOptions options, IMapper mapper)
        {
            _logger = logger;
            _options = options;
            _mapper = mapper;
            _sourceFilePath = "Files\\TransformationFiles\\";
            _destinationFilePath = "Files\\Archived\\";
            _outputFilePath = "Files\\ProcessedFile\\";
            _templateFilePath = "Files\\Template\\";
            CreateDirectory();
        }

        public void DoWork()
        {
            _logger.LogInformation("File Processing Start");
            FileProcessor();
        }

        public void FileProcessor()
        {
            FileInfo[] files = FileHelper.GetAllFiles(_sourceFilePath);

            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                 ProcessFile(file.FullName);    
                }
            }
            else
            {
                _logger.LogInformation($"No File to process");
            }

            
        }

        private void ProcessFile(string fileName)
        {
            var sourceData = ExtractDataFromFile<ClientAModel, ClientAColumnConfig>(fileName);
            var transformedData = TransformData<ClientAModel, ClientAOutputModel>(sourceData);
            var templatePath = (FileHelper.GetFileByName(_templateFilePath, "Template")).FullName;
            LoadData<ClientAOutputModel, ClientAColumnConfig>(transformedData, "ClientA", templatePath, fileName);
        }

        private List<TModel> ExtractDataFromFile<TModel,TConfig>(string filePath)
        {
            _logger.LogInformation($"Extracting data from file");
            return ExcelHelper.ReadFile<TModel,TConfig>(filePath);
        }

        private List<TDestination> TransformData<TSource, TDestination>(List<TSource> data)
        {
            _logger.LogInformation($"Transforming data");
            return _mapper.Map<List<TDestination>>(data);
        }

        private void LoadData<TModel,TConfig>(List<TModel> data, string clientName, string templateFile, string sourceFile)
        {
            _logger.LogInformation($"Loading data into file");
            ExcelHelper.WriteFile<TModel,TConfig>(templateFile, data,
                _outputFilePath + clientName + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx");

            _logger.LogInformation($"Moving source file to archived directory");
            FileHelper.MoveFile(clientName, sourceFile, _sourceFilePath, _destinationFilePath);
        }


        #region Helper Method

        private void CreateDirectory()
        {
            var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), _sourceFilePath);
            var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), _destinationFilePath);
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), _outputFilePath);
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), _templateFilePath);
            if (!Directory.Exists(sourcePath)) Directory.CreateDirectory(sourcePath);
            if (!Directory.Exists(destinationPath)) Directory.CreateDirectory(destinationPath);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);
            if (!Directory.Exists(templatePath)) Directory.CreateDirectory(templatePath);
        }

        public string GetClientNameForFile(string fileName)
        {
            return _options.Clients.FirstOrDefault(fileName.Contains);
        }

        #endregion
    }
}
