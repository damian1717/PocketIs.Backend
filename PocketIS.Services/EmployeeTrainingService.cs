using PocketIS.Domain;
using PocketIS.Domain.BusinessEntity;
using PocketIS.Domain.EmployeeTrainingModel;
using PocketIS.Infrastucture.Extensions;
using PocketIS.Repositories.Interfaces;
using PocketIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketIS.Services
{
    public class EmployeeTrainingService : IEmployeeTrainingService
    {
        private readonly IEmployeeTrainingRepository _employeeTrainingRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IOrganizationChartService _organizationChartService;

        public EmployeeTrainingService(IEmployeeTrainingRepository employeeTrainingRepository, ITrainingRepository trainingRepository,
            IEmployeeService employeeService, IOrganizationChartService organizationChartService)
        {
            _employeeTrainingRepository = employeeTrainingRepository;
            _trainingRepository = trainingRepository;
            _employeeService = employeeService;
            _organizationChartService = organizationChartService;
        }
        public async Task AddEmployeeTrainingAsync(EmployeeTraining employeeTraining)
            => await _employeeTrainingRepository.AddEmployeeTrainingAsync(employeeTraining);

        public async Task DeleteEmployeeTrainingAsync(Guid id)
            => await _employeeTrainingRepository.DeleteEmployeeTrainingAsync(id);

        public async Task<EmployeeTrainingForDisplayInfo> GetAllEmployeeTrainingsAsync()
        {
            var result = new EmployeeTrainingForDisplayInfo
            {
                Data = new List<EmployeeTrainingDataInfo>(),
                Columns = new List<ColumnInfo>
                {
                    new ColumnInfo { Name = "position", DisplayName =""},
                    new ColumnInfo { Name = "trainingName", DisplayName = "Nazwa Szkolenia"}
                }
            };

            var trainings = await _trainingRepository.GetTrainingsAsync();

            if (trainings is null || trainings.Count == 0) return result;

            var employees = await GetEmployeesAsync();

            if (employees.Count < 1) return result;

            foreach (var employee in employees)
            {
                result.Columns.Add(new ColumnInfo { Name = employee.ColumnName, DisplayName = employee.EmployeeName });
            }

            var employeeTrainings = await _employeeTrainingRepository.GetEmployeeTrainingsAsync();

            int counter = 1;
            foreach(var training in trainings)
            {
                var newEmployeeList = employees.DeepClone();

                newEmployeeList.ForEach(e =>
                {
                    var foundEmployeeTrainings = employeeTrainings
                                                .FirstOrDefault(x => x.TrainingId == training.Id 
                                                && x.EmployeeId == e.EmployeeId
                                                && x.EmployeeType.Equals(e.EmployeeType, StringComparison.OrdinalIgnoreCase));

                    if (foundEmployeeTrainings?.TrainingDate == null)
                    {
                        e.TrainingDate = "X";
                    }
                    else
                    {
                        e.TrainingDate = foundEmployeeTrainings.TrainingDate.ToString("dd/MM/yyyy");

                        TimeSpan t = foundEmployeeTrainings.TrainingDate.AddMonths(training.ForHowManyMonths) - DateTime.Now;

                        if (t.Days <= 0)
                        {
                            e.ClassOfDisplayTrainingDate = "training_after_term";
                        }
                        else if (t.Days > 0 && t.Days <= 30)
                        {
                            e.ClassOfDisplayTrainingDate = "training_near_term";
                        }

                        if (foundEmployeeTrainings.SkillLevel == 1)
                        {
                            e.ClassOfDisplayTrainingLevel = "training_level_basic";
                        }
                        else if (foundEmployeeTrainings.SkillLevel == 2)
                        {
                            e.ClassOfDisplayTrainingLevel = "training_level_pre_intermidiate";
                        }
                        else if (foundEmployeeTrainings.SkillLevel == 3)
                        {
                            e.ClassOfDisplayTrainingLevel = "training_level_intermidiate";
                        }
                        else if (foundEmployeeTrainings.SkillLevel == 4)
                        {
                            e.ClassOfDisplayTrainingLevel = "training_level_expert";
                        }
                    }
                });

                result.Data.Add(new EmployeeTrainingDataInfo
                {
                    Position = counter,
                    TrainingName = training.Name,
                    EmployeeTrainings = newEmployeeList
                });

                counter++;
            }

            return result;
        }

        public async Task<List<EmployeeTrainingRow>> GetEmployeesAsync()
        {
            var result = new List<EmployeeTrainingRow>();
            var employees = await _employeeService.GetEmployeesAsync() ?? new List<Employee>();

            foreach (var item in employees)
            {
                result.Add(new EmployeeTrainingRow
                {
                    EmployeeName = $"{item.Name} {item.LastName}",
                    ColumnName = $"{item.Id}_{"employee"}",
                    EmployeeId = item.Id,
                    EmployeeType = "new"
                });
            }

            var employeeOrgChar = await _organizationChartService.GetListOfPersonsAsync() ?? new List<OrganizationChartPersonModel>();

            foreach (var item in employeeOrgChar)
            {
                result.Add(new EmployeeTrainingRow
                {
                    EmployeeName = $"{item.Name} {item.LastName}",
                    ColumnName = $"{item.Id}_{"employeechar"}",
                    EmployeeId = item.Id,
                    EmployeeType = "old"
                });
            }

            return result;
        }

        public async Task<EmployeeTraining> GetEmployeeTrainingByIdAsync(Guid id)
            => await _employeeTrainingRepository.GetEmployeeTrainingByIdAsync(id);

        public async Task<List<EmployeeTrainingInfo>> GetEmployeeTrainingsAsync(Guid employeeId, int level)
        {
            var trainings = await _trainingRepository.GetTrainingsForLevelAsync(level);

            if (trainings is null || trainings.Count == 0) return new List<EmployeeTrainingInfo>();

            var employeeTrainings = await _employeeTrainingRepository.GetEmployeeTrainingsAsync(employeeId);

            var employeeTrainingList = new List<EmployeeTrainingInfo>();

            if (employeeTrainings is null || employeeTrainings.Count == 0)
            {
                foreach (var training in trainings)
                {
                    employeeTrainingList.Add(
                        new EmployeeTrainingInfo
                        {
                            Id = null,
                            Name = training.Name,
                            TrainingId = training.Id,
                            Required = false,
                            TrainingDate = null,
                            SkillLevel = null,
                            Finished = false
                        });
                }
            }
           else
                {
                    foreach (var training in trainings)
                    {
                        var employeeTraining = employeeTrainings.FirstOrDefault(x => x.TrainingId == training.Id);
                        employeeTrainingList.Add(
                        new EmployeeTrainingInfo
                                                {
                            Id = employeeTraining?.Id ?? null,
                            Name = training.Name,
                            TrainingId = training.Id,
                            Required = employeeTraining?.Required ?? false,
                            TrainingDate = employeeTraining?.TrainingDate ?? null,
                            SkillLevel = employeeTraining?.SkillLevel ?? null,
                            Finished = employeeTraining is not null
                        });
                    }
                }

            return employeeTrainingList;
        }

        public async Task UpdateEmployeeTrainingAsync(EmployeeTraining employeeTraining)
            => await _employeeTrainingRepository.UpdateEmployeeTrainingAsync(employeeTraining);
    }
}