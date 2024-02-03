using OrgChart;
using PocketIS.Domain;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class DefinitionOfProcessService : IDefinitionOfProcessService
    {
        private readonly IDefinitionOfProcessRepository _definitionOfProcessRepository;
        public DefinitionOfProcessService(IDefinitionOfProcessRepository definitionOfProcessRepository)
        {
            _definitionOfProcessRepository = definitionOfProcessRepository;
        }

        public async Task AddDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess) 
            => await _definitionOfProcessRepository.AddDefinitionOfProcessAsync(definitionProcess);
                
        public async Task<List<DefinitionOfProcess>> GetBaseDefinitionOfProcessAsync(Guid processId)
            => await _definitionOfProcessRepository.GetBaseDefinitionOfProcessAsync(processId);

        public async Task<List<DefinitionOfProcess>> GetDefinitionOfProcessAsync(Guid processId)
            => await _definitionOfProcessRepository.GetDefinitionOfProcessAsync(processId);

        public async Task<DefinitionOfProcess> GetDefinitionOfProcessByIdAsync(Guid id)
            => await _definitionOfProcessRepository.GetDefinitionOfProcessByIdAsync(id);

        public async Task UpdateDefinitionOfProcessAsync(DefinitionOfProcess definitionProcess)
            => await _definitionOfProcessRepository.UpdateDefinitionOfProcessAsync(definitionProcess);

        public async Task DeleteDefinitionOfProcessAsync(Guid id) 
            => await _definitionOfProcessRepository.DeleteDefinitionOfProcessAsync(id);

        public string GenerateDefinitionOfProcessImage64String(string processName)
        {
            OrgChartOption defaultOption = new OrgChartOption
            {
                BoxFillColor = ColorTranslator.FromHtml("#4DB6AC"),
                BoxBorderColor = ColorTranslator.FromHtml("#4DB6AC"),
                ConnectLineColor = ColorTranslator.FromHtml("#424242")
            };

            OrgChartOption option = new OrgChartOption()
            {
                BoxFillColor = defaultOption.BoxFillColor,
                BoxBorderColor = defaultOption.BoxBorderColor,
                ConnectLineColor = defaultOption.ConnectLineColor,
                FontSize = 9,
                HorizontalSpace = 10,
                VerticalSpace = 20,
                BoxHeight = 45,
                BoxWidth = 62,
                FontName = "Roboto, Helvetica, sans-serif"
            };

            var nodes = GetModel(processName);

            if (nodes is null && !nodes.Any()) return string.Empty;

            OrgChartGenerator orgChartGenerator = new OrgChartGenerator(nodes, option) { DefaultOption = defaultOption };

            var imageInByte = orgChartGenerator.Generate().ToArray();
            var string64file = Convert.ToBase64String(imageInByte);
            return $"data:image/png;base64, {string64file}";
        }

        private List<OrgChartNode> GetModel(string processName)
        {
            List<OrgChartNode> nodes = new List<OrgChartNode>();

            var children = GetChildren(GetId()).ToArray();

            var mainNode = new OrgChartNode(GetId(), $"{processName}", children);

            nodes.Add(mainNode);

            return nodes;
        }

        private List<OrgChartNode> GetChildren(string id)
        {
            List<OrgChartNode> children = new List<OrgChartNode>();

            var inputDataChild = new OrgChartNode(GetId(id, 1), "Dane wejściowe");
            children.Add(inputDataChild);

            var outputDataChild = new OrgChartNode(GetId(id, 2), "Mierniki");
            children.Add(outputDataChild);

            var measuredValuesDataChild = new OrgChartNode(GetId(id, 3), "Kto");
            children.Add(measuredValuesDataChild);

            var materialDataChild = new OrgChartNode(GetId(id, 4), "Co");
            children.Add(materialDataChild);

            var resourcesDataChild = new OrgChartNode(GetId(id, 5), "Jak");
            children.Add(resourcesDataChild);

            var documentsDataChild = new OrgChartNode(GetId(id, 6), "Cel procesu");
            children.Add(documentsDataChild);

            var itemsDataChild = new OrgChartNode(GetId(id, 7), "Dane wyjściowe");
            children.Add(itemsDataChild);

            return children;
        }

        private string GetId(string parentId = "", int numberOfChild = 0)
        {
            if (string.IsNullOrEmpty(parentId)) return "0";

            if (parentId.Length == 1)
            {
                int.TryParse(parentId, out int parsedId);

                if (parsedId == 0)
                {
                    parsedId = 1;
                }

                return $"{parsedId}.{numberOfChild}";
            }
            else if (parentId.Length == 3)
            {
                return $"{parentId}.{numberOfChild}";
            }
            else if (parentId.Length == 5)
            {
                return $"{parentId}.{numberOfChild}";
            }
            else if (parentId.Length == 7)
            {
                return $"{parentId}.{numberOfChild}";
            }
            else if (parentId.Length == 9)
            {
                return $"{parentId}.{numberOfChild}";
            }
            else if (parentId.Length == 11)
            {
                return $"{parentId}.{numberOfChild}";
            }
            else if (parentId.Length == 13)
            {
                return $"{parentId}.{numberOfChild}";
            }
            else if (parentId.Length == 15)
            {
                return $"{parentId}.{numberOfChild}";
            }

            return "";
        }
    }
}
