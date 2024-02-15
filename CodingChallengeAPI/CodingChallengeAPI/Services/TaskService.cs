using AutoMapper;
using CodingChallengeAPI.Database;
using CodingChallengeAPI.DTO;
using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CodingChallengeAPI.Services
{
    public class TaskService:ITaskService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public TaskService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;

        }
        public void AddTask(TaskEntity task)
        {
            try
            {
                context.Tasks.Add(task);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public List<TaskEntity> GetAllTasks()
        {
            try
            {
                List<TaskEntity> allTasks = context.Tasks.ToList();

                return allTasks;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public TaskEntity GetTaskById(int id)
        {

            try
            {
                return context.Tasks.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TaskDTO> GetTasksByUserId(int userId)
        {
            try
            {
                List<TaskEntity> tasks = context.Tasks.Where(p => p.UserId == userId).ToList();

                List<TaskDTO> taskdtos = _mapper.Map<List<TaskDTO>>(tasks);


                return taskdtos;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ResultModel EditTask(TaskDTO newtask)
        {
            try
            {
                TaskEntity existingtask = context.Tasks.SingleOrDefault(u => u.TaskId == newtask.TaskId);
                TaskEntity newdto = _mapper.Map<TaskEntity>(newtask);
                if (existingtask != null)
                {
                    context.Entry(existingtask).State = EntityState.Detached;
                    context.Tasks.Update(newdto);
                    context.SaveChanges();
                    return new ResultModel { Success = true, Message = "Task edited successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "Task not found." };
                }
            }
            catch (Exception ex)
            {

                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        public ResultModel DeleteTask(int taskId)
        {
            try
            {
                TaskEntity task = context.Tasks.SingleOrDefault(p => p.TaskId == taskId);
                
                if (task != null)
                {
                    context.Tasks.Remove(task);
                    context.SaveChanges();

                    return new ResultModel { Success = true, Message = "Task deleted successfully." };
                }
                else
                {
                    return new ResultModel { Success = false, Message = "Task not found." };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel { Success = false, Message = $"Error: {ex.Message}" };
            }
        }
        public void MarkAsRead(int taskId)
        {
            try
            {
                var task = context.Tasks.Find(taskId);
                task.Completed = 1;
                task.Status = "Completed";
                task.CompletedDate = DateTime.Now;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }

}
