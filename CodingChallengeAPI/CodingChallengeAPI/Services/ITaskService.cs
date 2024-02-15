using CodingChallengeAPI.DTO;
using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Model;

namespace CodingChallengeAPI.Services
{
    public interface ITaskService
    {
        void AddTask(TaskEntity task);
        List<TaskEntity> GetAllTasks();
        TaskEntity GetTaskById(int id);
        List<TaskDTO> GetTasksByUserId(int userId);
        ResultModel DeleteTask(int taskId);
        ResultModel EditTask(TaskDTO newtask);
        void MarkAsRead(int taskId);
    }
}
