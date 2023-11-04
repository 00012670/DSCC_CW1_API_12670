using API.DAL;
using API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; 
using System.Linq;

namespace API.Repositories
{
    // Defining the ProgressRepository class that implements IProgressRepository interface
    public class ProgressRepository : IProgressRepository
    {
        // Private field to hold the DataContext instance
        private readonly DataContext _dbContext;

        // Constructor that takes a DataContext instance as a parameter
        public ProgressRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Save changes to the database
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        // Get all Progresses from the database, including related Habits
        public IEnumerable<Progress> GetProgresses()
        {
            // Return all Progresses with related Habits as a list
            return _dbContext.Progresses.Include(s => s.Habit).ToList();
        }

        // Find a Progress by its ID in the database
        private Progress FindProgressById(int progressId)
        {
            return _dbContext.Progresses.Find(progressId);
        }

        // PGet a Progress by its ID, including the related Habit
        public Progress GetProgressById(int Id)
        {
            var progress = FindProgressById(Id);

            // Load the related Habit for the retrieved Progress
            _dbContext.Entry(progress).Reference(s => s.Habit).Load();

            // Return the found Progress
            return progress;
        }

        // Public method to insert a new Progress into the database
        public void InsertProgress(Progress progress)
        {
            // If the Progress has a related Habit, ensure it's attached to the DbContext
            if (progress.Habit != null)
            {
                progress.Habit = _dbContext.Habits.FirstOrDefault(h => h.ID == progress.Habit.ID);
            }

            // Add the new Progress to the database
            _dbContext.Add(progress);
            Save();
        }

        // Public method to delete a Progress by its ID
        public void DeleteProgress(int progressId)
        {
            // Find the Progress by ID
            var progress = FindProgressById(progressId);

            // Remove the Progress from the database
            _dbContext.Progresses.Remove(progress);
            Save();
        }

        // Update an existing Progress in the database
        public void UpdateProgress(Progress progress)
        {
            // Find the existing Progress by its ID
            var existingProgress = _dbContext.Progresses.Find(progress.ID);

            // Update the existing Progress's properties with the new values
            existingProgress.HabitProgress = progress.HabitProgress;
            existingProgress.IsCompleted = progress.IsCompleted;
            existingProgress.Note = progress.Note;
            existingProgress.EndDate = progress.EndDate;

            // Save changes to the database
            Save();
        }
    }
}
