using System;

public class Sneaking
{
    public static void Main()
    {
        var roomRows = int.Parse(Console.ReadLine());
        var room = new char[roomRows][];

        for (var i = 0; i < roomRows; i++)
        {
            var curRow = Console.ReadLine().ToCharArray();
            room[i] = curRow;
        }

        var commands = Console.ReadLine().ToCharArray();

        foreach (var command in commands)
        {
            MoveEnemies(room);

            if (EnemyKillsSam(room))
            {
                var samLastCoordiantes = GetSamCoordinates(room);
                Console.WriteLine($"Sam died at {samLastCoordiantes[0]}, {samLastCoordiantes[1]}");
                room[samLastCoordiantes[0]][samLastCoordiantes[1]] = 'X';
                PrintRoom(room);
                break;
            }

            MoveSam(room, command);
            if (SamKillsNikoladze(room))
            {
                Console.WriteLine("Nikoladze killed!");
                PrintRoom(room);
                break;
            }
        }
    }

    private static bool SamKillsNikoladze(char[][] room)
    {
        var samsCoordinates = GetSamCoordinates(room);
        var nikoladzeCoordiantes = GetNikoladzeCoordiantes(room);

        if (samsCoordinates[0] == nikoladzeCoordiantes[0])
        {
            room[nikoladzeCoordiantes[0]][nikoladzeCoordiantes[1]] = 'X';
            return true;
        }

        return false;
    }

    private static void PrintRoom(char[][] room)
    {
        foreach (var row in room)
        {
            Console.WriteLine(string.Join("", row));
        }
    }

    private static bool EnemyKillsSam(char[][] room)
    {
        var samsCoordiantes = GetSamCoordinates(room);
        var samRow = samsCoordiantes[0];
        var samCol = samsCoordiantes[1];

        var rowSamIsOn = room[samRow];

        foreach (var spot in rowSamIsOn)
        {
            switch (spot)
            {
                case 'b' when Array.IndexOf(rowSamIsOn, spot) < samCol:
                    return true;
                case 'd' when Array.IndexOf(rowSamIsOn, spot) > samCol:
                    return true;
            }
        }

        return false;
    }

    private static void MoveEnemies(char[][] room)
    {
        for (var r = 0; r < room.Length; r++)
        {
            var flipped = false;
            var moved = false;
            for (var c = 0; c < room[r].Length; c++)
            {
                var currentEnemy = room[r][c];

                if (currentEnemy == '.') continue;

                if (currentEnemy == 'b' && c == room[r].Length - 1 && !moved)
                {
                    room[r][c] = 'd';
                    flipped = true;
                }

                if (currentEnemy == 'b' && c < room[r].Length - 1 && !flipped && !moved)
                {
                    room[r][c] = '.';
                    room[r][c + 1] = 'b';
                    moved = true;
                }

                if (currentEnemy == 'd' && c > 0)
                {
                    room[r][c] = '.';
                    room[r][c - 1] = 'd';
                }

                if (currentEnemy == 'd' && c == 0 && !flipped && !moved)
                {
                    room[r][c] = 'b';
                    flipped = true;
                }
            }
        }
    }

    private static void MoveSam(char[][] room, char command)
    {
        var samCurCoordinates = GetSamCoordinates(room);
        var samRow = samCurCoordinates[0];
        var samCol = samCurCoordinates[1];

        if (command == 'W') return;

        room[samRow][samCol] = '.';

        var samKilledEnemy = false;

        switch (command)
        {
            case 'U':
                room[samRow - 1][samCol] = 'S';
                break;
            case 'D':
                room[samRow + 1][samCol] = 'S';
                break;
            case 'L':
                room[samRow][samCol - 1] = 'S';
                break;
            case 'R':
                room[samRow][samCol + 1] = 'S';
                break;
            default:
                throw new Exception();
        }
    }

    private static int[] GetSamCoordinates(char[][] room)
    {
        var x = 0;
        var y = 0;

        var foundSam = false;
        for (var r = 0; r < room.Length; r++)
        {
            for (var c = 0; c < room[r].Length; c++)
            {
                if (room[r][c] == 'S')
                {
                    x = r;
                    y = c;
                    foundSam = true;
                    break;
                }
            }

            if (foundSam) break;
        }

        return new[] { x, y };
    }

    private static int[] GetNikoladzeCoordiantes(char[][] room)
    {
        var x = 0;
        var y = 0;

        var foundNikoladze = false;
        for (var r = 0; r < room.Length; r++)
        {
            for (var c = 0; c < room[r].Length; c++)
            {
                if (room[r][c] == 'N')
                {
                    x = r;
                    y = c;
                    foundNikoladze = true;
                    break;
                }
            }

            if (foundNikoladze) break;
        }

        return new[] { x, y };
    }
}