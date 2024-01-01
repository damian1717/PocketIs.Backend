using PocketIS.Domain;
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
        public EmployeeTrainingService(IEmployeeTrainingRepository employeeTrainingRepository, ITrainingRepository trainingRepository)
        {
            _employeeTrainingRepository = employeeTrainingRepository;
            _trainingRepository = trainingRepository;
        }
        public async Task AddEmployeeTrainingAsync(EmployeeTraining employeeTraining)
            => await _employeeTrainingRepository.AddEmployeeTrainingAsync(employeeTraining);

        public async Task DeleteEmployeeTrainingAsync(Guid id)
            => await _employeeTrainingRepository.DeleteEmployeeTrainingAsync(id);

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
