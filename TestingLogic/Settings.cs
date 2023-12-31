﻿using Application.DTOs;
using IndentityLogic.DTOs;
using Microsoft.Data.Sqlite;

namespace TestingLogic
{
    public class Settings
    {
        public string BaseAddress = "https://localhost:7177";
        public  string JackToken = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImphY2siLCJuYW1laWQiOiI4MjI0NTc4NC05NjFmLTRlNjYtNTFkMC0wOGRiYmUwNWVjMjgiLCJlbWFpbCI6ImphY2tAdGFjay5jb20iLCJuYmYiOjE2OTkyNjUzMTQsImV4cCI6MTY5OTg3MDExNCwiaWF0IjoxNjk5MjY1MzE0fQ.f_8iUnCzQ4JCEss_lz6chdk0t74l2l3U9fIQngT38SBB_6ADDG5fBCiuXxYTY3weEpRnzStshQJskU9h51zAXw";
        public  string RonToken = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InJvbiIsIm5hbWVpZCI6ImM2ODU4ZWYxLWI1NDktNDIwYy01MWQyLTA4ZGJiZTA1ZWMyOCIsImVtYWlsIjoicm9uQHRhY2suY29tIiwibmJmIjoxNjk5MjY3NTIyLCJleHAiOjE2OTk4NzIzMjIsImlhdCI6MTY5OTI2NzUyMn0.tH5DqGbmwBeWo2nMnRWT3zIfiZ4DX7hOsJEWMwOg_zCvPn6ZgwcY8rkmoqt4GIgOYeIU85oQU-3JZkxnDlcM5A";
        public  string InmemoryToken = null;
        public  SqliteConnection Connection = new SqliteConnection("Data Source=:memory:");
        public  UserDto userDto = null;
        public NotebookDto NotebookDto = null;
        public       UnitDto UnitDto = null;
        public PageDto PageDto = null;
        public  NoteDto NoteDto = null;
    }
}