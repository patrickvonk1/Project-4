using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseAssembly//ToDo: Favourite page, User Inlog + Register pagina, Instellingen, Instellingen:User pagina veranderen(Profile Image, Name) user kunnen lines zelf maken en publishen en gerate worden door andere.|||| Alle pages nice layout geven.
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        //Used for the methods who return a random line
        static Random random = new Random();

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            //All the tables, This is where they are created
            database.CreateTableAsync<PickupLine>().Wait();
            database.CreateTableAsync<MotivationLine>().Wait();
            database.CreateTableAsync<JokeLine>().Wait();
        }

        #region PickupLine Methods

        public async Task<List<PickupLine>> GetPickupLinesAsync()
        {
            return await database.Table<PickupLine>().ToListAsync();
        }

        public async Task<PickupLine> GetRandomPickupLineAsync()
        {
            List<PickupLine> allPickupLines = new List<PickupLine>(await database.Table<PickupLine>().ToListAsync());
            if (allPickupLines.Count > 0)
            {
                return allPickupLines[random.Next(0, allPickupLines.Count)];
            }

            //There are no pickupLines in the database!
            return null;
        }

        public async Task<PickupLine> GetPickupLineByFilter(string pickupLineType)
        {
            List<PickupLine> allPickupLine = await database.Table<PickupLine>().ToListAsync();
            List<PickupLine> filteredPickupLines = new List<PickupLine>();

            foreach (var pickupLine in allPickupLine)
            {
                if (pickupLine.PickupLineType == (PickupLineType)Enum.Parse(typeof(PickupLineType), pickupLineType))
                {
                    filteredPickupLines.Add(pickupLine);
                }
            }

            if (filteredPickupLines.Count == 0)
            {
                return null;
            }

            return filteredPickupLines[random.Next(0, filteredPickupLines.Count)];
        }

        public async Task<PickupLine> GetPickupLineAsync(int id)
        {
            return await database.Table<PickupLine>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SavePickupLineAsync(PickupLine newPickupLine)
        {
            if (newPickupLine.ID == 0)//Add new pickupLine
            {
                int result = await database.InsertAsync(newPickupLine);
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else// Update this pickupline, it already exists
            {
                int result = await database.UpdateAsync(newPickupLine);
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeletePickupLineAsync(PickupLine pickupLineToDelete)
        {
            int result = await database.DeleteAsync(pickupLineToDelete);

            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region MotivationLine Methods

        public async Task<List<MotivationLine>> GetMotivationLinesAsync()
        {
            return await database.Table<MotivationLine>().ToListAsync();
        }

        public async Task<MotivationLine> GetRandomMotivationLineAsync()
        {
            List<MotivationLine> allMotivationLines = new List<MotivationLine>(await database.Table<MotivationLine>().ToListAsync());

            return allMotivationLines.Count > 0 ? allMotivationLines[random.Next(0, allMotivationLines.Count)] : null;

            //if (allMotivationLines.Count > 0)
            //{
            //    return allMotivationLines[random.Next(0, allMotivationLines.Count)];
            //}

            ////There are no pickupLines in the database!
            //return null;
        }

        public async Task<MotivationLine> GetMotivationLineByFilter(string motivationLineType)
        {
            List<MotivationLine> allMotivationLine = await database.Table<MotivationLine>().ToListAsync();
            List<MotivationLine> filteredMotivationLines = new List<MotivationLine>();

            foreach (var motivationLine in allMotivationLine)
            {
                if (motivationLine.MotivationLineType == (MotivationLineType)Enum.Parse(typeof(MotivationLineType), motivationLineType))
                {
                    filteredMotivationLines.Add(motivationLine);
                }
            }

            if (filteredMotivationLines.Count == 0)
            {
                return null;
            }

            return filteredMotivationLines[random.Next(0, filteredMotivationLines.Count)];
        }

            public async Task<MotivationLine> GetMotivationLineAsync(int id)
        {
            return await database.Table<MotivationLine>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveMotivationLineAsync(MotivationLine newMotivationLine)
        {
            if (newMotivationLine.ID == 0)//Add new motivationline
            {
                int result = await database.InsertAsync(newMotivationLine);
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else// Update this motivationLine, it already exists
            {
                int result = await database.UpdateAsync(newMotivationLine);
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteMotivationLineAsync(MotivationLine motivationLineToDelete)
        {
            int result = await database.DeleteAsync(motivationLineToDelete);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region JokeLine Methods
        
        public async Task<List<JokeLine>> GetJokeLinesAsync()
        {
            return await database.Table<JokeLine>().ToListAsync();
        }

        public async Task<JokeLine> GetRandomJokeLineAsync()
        {
            List<JokeLine> allJokeLinesList = new List<JokeLine>(await database.Table<JokeLine>().ToListAsync());

            return allJokeLinesList.Count > 0 ? allJokeLinesList[random.Next(0, allJokeLinesList.Count)] : null;

            //if (allMotivationLines.Count > 0)
            //{
            //    return allMotivationLines[random.Next(0, allMotivationLines.Count)];
            //}

            ////There are no pickupLines in the database!
            //return null;
        }

        public async Task<JokeLine> GetJokeLineByFilter(string jokeLineType)
        {
            List<JokeLine> allJokeLine = await database.Table<JokeLine>().ToListAsync();
            List<JokeLine> filteredJokeLines = new List<JokeLine>();

            foreach (var jokeLine in allJokeLine)
            {
                if (jokeLine.JokeLineType == (JokeLineType)Enum.Parse(typeof(JokeLineType), jokeLineType))
                {
                    filteredJokeLines.Add(jokeLine);
                }
            }

            if (filteredJokeLines.Count == 0)
            {
                return null;
            }

            return filteredJokeLines[random.Next(0, filteredJokeLines.Count)];
        }

        public async Task<JokeLine> GetJokeLineAsync(int id)
        {
            return await database.Table<JokeLine>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveJokeLineAsync(JokeLine newJokeLine)
        {
            if (newJokeLine.ID == 0)//Add new jokeLine
            {
                int result = await database.InsertAsync(newJokeLine);
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else// Update this jokeLine, it already exists
            {
                int result = await database.UpdateAsync(newJokeLine);
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteJokeLineAsync(JokeLine jokeLineToDelete)
        {
            int result = await database.DeleteAsync(jokeLineToDelete);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
