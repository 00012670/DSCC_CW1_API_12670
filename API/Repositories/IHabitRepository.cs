﻿using API.Models;

namespace API.Repositories
{
    public interface IHabitRepository
    {
        void InsertHabit(Habit habit);
        void UpdateHabit(Habit habit);
        void DeleteHabit(int habitId);
        Habit GetHabitById(int Id);
        IEnumerable<Habit> GetHabits();
    }
}
