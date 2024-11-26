# Human Benchmark

This solution is designed to measure and improve users' reaction time. 

## Team Members:
- Edvinas Burba
- Eimantas Labžentis
- Martynas Klimas

## Technologies Used
Front-end:
- React

Back-end:
- .NET 8
- C#
- Entity Framework Core
- PostgreSQL

## Deployment on Local Machines
Checkout the Github Repository
Setup PostgreSQL in Docker using:
```
docker run -itd -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 --name postgresql postgres
```
Then run the following commands
Using .NET 8 SKD:
```
cd AimReactionAPI
dotnet run
```
or using Docker Compose
```
cd AimReactionAPI
docker-compose up --build
```

## Project Structure
### Main Components
- ```User```
- ```Target```
- ```GameConfig```
- ```Score```

### Data Flow
1. User Uploads Config File (Frontend to Backend):
   - User uploads a game configuration file through the frontend.
   - Frontend sends a ```POST``` request to backend.
   - Backend processed the file, parses it and validates it.
   - The validated configuration file is saved in the database.
2. Game Initiation and Interaction:
   - User starts a game.
   - Frontend sends a ```GET``` request.
   - Backend creates a game session, retrieves necessary configurations and sends game-specific data to the frontend.
   - User interacts with the game.
   - When the game ends, score is submitted.
   - Score is validated and saved in the database.
3. Leaderboard Data Fetch:
   - Frontend fetches leaderboard data
   - Backend queries the DB and returns the top scores for display.
  
## Use-Case Diagram
![image](https://github.com/user-attachments/assets/aee5af79-8ee2-471e-a13a-24ee1c472ee1)

## DB Overview
![image](https://github.com/user-attachments/assets/dfcf0d00-4ab5-42b7-b97d-0caf1f3ec614)
