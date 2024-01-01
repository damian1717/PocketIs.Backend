using OrgChart;
using PocketIS.Application.Common.Codes;
using PocketIS.Application.Common.Types;
using PocketIS.Domain;
using PocketIS.Domain.BusinessEntity;
using PocketIS.Domain.ChartOrg;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class OrganizationChartService : IOrganizationChartService
    {
        private readonly IOrganizationChartRepository _organizationChartRepository;
        public OrganizationChartService(IOrganizationChartRepository organizationChartRepository)
        {
            _organizationChartRepository = organizationChartRepository;
        }

        public async Task AddOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson) 
            => await _organizationChartRepository.AddOrganizationChartPersonAsync(organizationChartPerson);

        public async Task<OrganizationChartPerson> GetPersonAsync(Guid id) 
            => await _organizationChartRepository.GetPersonAsync(id);

        public async Task<List<OrganizationChartPersonModel>> GetListOfPersonsAsync()
        {
            var newList = new List<OrganizationChartPersonModel>();
            
            var organizationChartPersons = await _organizationChartRepository.GetListOfPersonsAsync();
            
            var namesOfPersons = organizationChartPersons.ToDictionary(x => x.Id, z => $"{z.Name} {z.LastName}");

            foreach (var person in organizationChartPersons)
            {
                var belowPersonName = string.Empty;
                
                if (person.BelowPersonId is not null)
                {
                    var guid = (Guid)person.BelowPersonId;
                    belowPersonName = namesOfPersons.ContainsKey(guid) ? namesOfPersons[guid] : string.Empty;
                }

                var item = new OrganizationChartPersonModel
                {
                    Id = person.Id,
                    Name = person.Name,
                    BelowPersonId = person.BelowPersonId,
                    LastName = person.LastName,
                    Position = person.Position,
                    BelowPersonName = belowPersonName,
                    Level = person.Level,
                    Email = person.Email
                };
                newList.Add(item);
            }

            return newList;
        }

        public async Task UpdateOrganizationChartPersonAsync(OrganizationChartPerson organizationChartPerson) 
            => await _organizationChartRepository.UpdateOrganizationChartPersonAsync(organizationChartPerson);

        public async Task DeleteOrganizationChartPersonAsync(Guid id)
        {
            var allPersonsBelow = await GetListOfPersonsBelowIdAsync(id);

            if (allPersonsBelow is not null && allPersonsBelow.Any())
            {
                throw new PocketISException(OrganizationChartCodes.BelowPersonExists,
                    "Nie można usunąc danej osoby. Dana osoba jest w osobach poniżej.");
            }
            await _organizationChartRepository.DeleteOrganizationChartPersonAsync(id);
        }

        public async Task<List<OrganizationChartPerson>> GetListOfPersonsBelowIdAsync(Guid id) 
            => await _organizationChartRepository.GetListOfPersonsBelowIdAsync(id);

        public string GenerateOrganizationChartImage64String(ChartNode[] chartNodes)
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
                BoxWidth = 110,
                //UseMinBoxWidthWhenHasOnlyOne = true,
                //MinBoxWidth = 80
            };

            var nodes = GetOrgChartNodes(chartNodes);

            if (nodes is null && !nodes.Any()) return string.Empty;

            OrgChartGenerator orgChartGenerator = new OrgChartGenerator(nodes, option) { DefaultOption = defaultOption };

            var imageInByte = orgChartGenerator.Generate().ToArray();
            var string64file = Convert.ToBase64String(imageInByte);
            return $"data:image/png;base64, {string64file}";
        }

        public async Task<int> GetMaxLevelAsync() 
            => await _organizationChartRepository.GetMaxLevelAsync();

        private List<OrgChartNode> GetOrgChartNodes(ChartNode[] chartNodes)
        {
            List<OrgChartNode> nodes = new List<OrgChartNode>();

            if (chartNodes is null || chartNodes.Length <= 0) return nodes;

            var children = GetChildren(chartNodes[0], GetId()).ToArray();

            var mainNode = new OrgChartNode(GetId(), $"{chartNodes[0].Data.Name} {chartNodes[0].Data.Title}", children);

            nodes.Add(mainNode);

            return nodes;
        }

        private List<OrgChartNode> GetChildren(ChartNode node, string id)
        {
            List<OrgChartNode> children = new List<OrgChartNode>();

            for (int i = 0; i <node.Children.Length; i++)
            {
                var newId = GetId(id, i+ 1);
                var childChildren = GetChildren(node.Children[i], newId).ToArray();
                var newChild = new OrgChartNode(newId, $"{node.Children[i].Data.Name} {node.Children[i].Data.Title}", childChildren);
                
                children.Add(newChild);
            }

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
