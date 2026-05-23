/*
 * Author:      Ng Ern Chi
 * Description: Database utility helper (publish column check)
 * Date:        23/5/2026
 */
using System;
using System.Data;
using System.Data.SqlClient;

public static class DatabaseHelper
{
    public static void EnsureCoursePublishedColumn(SqlConnection conn)
    {
        if (conn == null) throw new ArgumentNullException("conn");

        bool shouldClose = conn.State != ConnectionState.Open;
        if (shouldClose) conn.Open();

        SqlCommand checkCmd = new SqlCommand(@"
            SELECT COUNT(*)
            FROM sys.columns
            WHERE object_id = OBJECT_ID('Courses')
              AND name = 'published'", conn);

        bool columnExists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;

        if (!columnExists)
        {
            SqlCommand addCmd = new SqlCommand(@"
                ALTER TABLE Courses
                ADD published BIT NOT NULL
                    CONSTRAINT DF_Courses_published DEFAULT(0)
                    WITH VALUES;", conn);
            addCmd.ExecuteNonQuery();

            SqlCommand publishExistingCmd = new SqlCommand(@"
                UPDATE c
                SET published = 1
                FROM Courses c
                WHERE EXISTS (SELECT 1 FROM Lessons l WHERE l.course_id = c.course_id)
                  AND EXISTS (SELECT 1 FROM Quizzes q WHERE q.course_id = c.course_id);", conn);
            publishExistingCmd.ExecuteNonQuery();
        }

        if (shouldClose) conn.Close();
    }
}
