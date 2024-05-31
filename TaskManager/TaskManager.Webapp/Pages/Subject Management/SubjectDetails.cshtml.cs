using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Repository;
using TaskManager.Application.Domain;
using Task = TaskManager.Application.Domain.Task;
using System.Net;

namespace TaskManager.Webapp.Pages {
    public class SubjectTasksModel : PageModel {
        private readonly TaskRepository _taskRepository;
        public List<Task> Tasks { get; set; }
        public string SubjectName { get; set; }

        public SubjectTasksModel(TaskRepository taskRepository) {
            _taskRepository = taskRepository;
        }

        public void OnGet(string subjectName) {
            SubjectName = WebUtility.UrlDecode(subjectName);
            Tasks = _taskRepository.GetTasksBySubject(SubjectName);
        }
    }
}