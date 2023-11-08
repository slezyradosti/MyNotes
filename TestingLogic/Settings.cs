using Application.DTOs;
using IndentityLogic.DTOs;
using Microsoft.Data.Sqlite;

namespace TestingLogic
{
    public static class Settings
    {
        public const string BaseAddress = "https://localhost:7177";
        public static string JackToken = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImphY2siLCJuYW1laWQiOiI4MjI0NTc4NC05NjFmLTRlNjYtNTFkMC0wOGRiYmUwNWVjMjgiLCJlbWFpbCI6ImphY2tAdGFjay5jb20iLCJuYmYiOjE2OTkyNjUzMTQsImV4cCI6MTY5OTg3MDExNCwiaWF0IjoxNjk5MjY1MzE0fQ.f_8iUnCzQ4JCEss_lz6chdk0t74l2l3U9fIQngT38SBB_6ADDG5fBCiuXxYTY3weEpRnzStshQJskU9h51zAXw";
        public static string RonToken = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InJvbiIsIm5hbWVpZCI6ImM2ODU4ZWYxLWI1NDktNDIwYy01MWQyLTA4ZGJiZTA1ZWMyOCIsImVtYWlsIjoicm9uQHRhY2suY29tIiwibmJmIjoxNjk5MjY3NTIyLCJleHAiOjE2OTk4NzIzMjIsImlhdCI6MTY5OTI2NzUyMn0.tH5DqGbmwBeWo2nMnRWT3zIfiZ4DX7hOsJEWMwOg_zCvPn6ZgwcY8rkmoqt4GIgOYeIU85oQU-3JZkxnDlcM5A";
        public static string InmemoryToken = null;
        public static SqliteConnection Connection = new SqliteConnection("Data Source=:memory:");
        public static UserDto userDto = null;
        public static NotebookDto NotebookDto = null;
    }
}