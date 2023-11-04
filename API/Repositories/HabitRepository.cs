using API.DAL;
using API.Models;

namespace API.Repositories
{
    public class HabitRepository : IHabitRepository
    {
        // Private field to hold the DataContext instance
        private readonly DataContext _dbContext;

        // Constructor that takes a DataContext instance as a parameter
        public HabitRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Find a Habit by its ID in the database
        private Habit FindHabitById(int habitId)
        {
            return _dbContext.Habits.Find(habitId);
        }

        // Save changes to the database
        private void Save()
        {
            _dbContext.SaveChanges();
        }

        // Get all Habits from the database
        public IEnumerable<Habit> GetHabits()
        {
            // Return all Habits as a list
            return _dbContext.Habits.ToList();
        }

        // Get a Habit by its ID
        public Habit GetHabitById(int Id)
        {
            // Find the Habit by ID
            var habit = FindHabitById(Id);

            // Return the found Habit
            return habit;
        }

        // Insert a new Habit into the database
        public void InsertHabit(Habit habit)
        {
            // Add the new Habit to the database
            _dbContext.Add(habit);
            Save();
        }

        // Delete a Habit by its ID
        public void DeleteHabit(int habitId)
        {
            // Find the Habit by ID
            var habit = FindHabitById(habitId);

            // Remove the Habit from the database
            _dbContext.Habits.Remove(habit);
            Save();
        }

        // Update an existing Habit in the database
        public void UpdateHabit(Habit habit)
        {
            // Find the existing Habit by its ID
            var existingHabit = _dbContext.Habits.Find(habit.ID);

            // Update the existing Habit's properties with the new values
            existingHabit.Name = habit.Name;
            existingHabit.Frequency = habit.Frequency;
            existingHabit.Repeat = habit.Repeat;
            existingHabit.StartDate = habit.StartDate;
            Save();
        }
    }
}
