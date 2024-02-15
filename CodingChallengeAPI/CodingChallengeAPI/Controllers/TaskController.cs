using AutoMapper;
using CodingChallengeAPI.DTO;
using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILog _logger;

        public TaskController(ITaskService taskService, IMapper mapper, IConfiguration configuration, ILog logger)
        {
            this.taskService = taskService;
            this._mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
        }
        [HttpPost, Route("AddTask")]
        [AllowAnonymous]
        public IActionResult AddTask(TaskWithOutIDDTO taskdto)
        {
            try
            {
                TaskEntity task = _mapper.Map<TaskEntity>(taskdto);
                task.CompletedDate = null;
                task.Status = "To-Do";
                task.CreatedDate= DateTime.Now;
                taskService.AddTask(task);
                return StatusCode(200, taskdto);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetAllTasks")]
        [AllowAnonymous]

        public IActionResult GetAllTasks()
        {
            try
            {
                List<TaskEntity> tasks = taskService.GetAllTasks();
                List<TaskDTO> taskDTOs = _mapper.Map<List<TaskDTO>>(tasks);
                return StatusCode(200, taskDTOs);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetTaskById/{taskId}")]
        [AllowAnonymous]
        //
        public IActionResult GetTaskById(int taskId)
        {
            try
            {
                TaskEntity task = taskService.GetTaskById(taskId);
                if (task != null)
                {
                    return StatusCode(200, task);
                }
                else
                {
                    _logger.Error($"Task not found");
                    return StatusCode(200);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("ListTasksByUserId/{userId}")]
        [AllowAnonymous]
        public IActionResult ListTasksByUserId(int userId)
        {
            try
            {

                List<TaskDTO> task = taskService.GetTasksByUserId(userId);
                return StatusCode(200, task);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut, Route("EditTask")]
        [AllowAnonymous]
        public IActionResult EditPost(TaskDTO taskdto)
        {
            try
            {
                var result = taskService.EditTask(taskdto);
                if (result.Success)
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }


        [HttpDelete, Route("DeleteTask/{TaskId}")]
        [AllowAnonymous]
        public IActionResult DeleteTask(int TaskId)
        {
            try
            {
                var result = taskService.DeleteTask(TaskId);
                if (result.Success)
                {
                    return StatusCode(200, result.Message);
                }
                else
                {

                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut, Route("MarkAsRead/{taskId}")]
        [AllowAnonymous]

        public IActionResult MarkAsRead(int taskId)
        {
            try
            {
                taskService.MarkAsRead(taskId);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
