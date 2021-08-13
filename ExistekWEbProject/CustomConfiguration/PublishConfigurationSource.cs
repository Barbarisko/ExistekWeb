using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomConfiguration
{
    public class PublishConfigurationSource : IConfigurationSource
    {
        private readonly string _filePath;

        public PublishConfigurationSource(string filePath)
        {
            _filePath = filePath;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var physicalPath = builder.GetFileProvider().GetFileInfo(_filePath).PhysicalPath;
            return new PublishConfigurationProvider(physicalPath);
        }
    }
}
