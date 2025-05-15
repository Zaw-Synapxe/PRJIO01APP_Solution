Create PROCEDURE [dbo].[spDataInDataTable] (  
    @sortColumn VARCHAR(50)  
    ,@sortOrder VARCHAR(50)  
    ,@OffsetValue INT  
    ,@PagingSize INT  
    ,@SearchText VARCHAR(50)  
    )  
AS  
BEGIN  
    SELECT ID  
        ,FullName  
        ,PhoneNumber  
        ,FaxNumber  
        ,EmailAddress  
        ,count(ID) OVER () AS FilterTotalCount  
    FROM Employee  
    WHERE (  
            (  
                @SearchText <> ''  
                AND (  
                    FullName LIKE '%' + @SearchText + '%'  
                    OR PhoneNumber LIKE '%' + @SearchText + '%'  
                    )  
                )  
            OR (@SearchText = '')  
            )  
    ORDER BY CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'FullName'  
                THEN FullName  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'Desc'  
                THEN ''  
            WHEN @sortColumn = 'FullName'  
                THEN FullName  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN 0  
            WHEN @sortColumn = 'ID'  
                THEN ID  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN 0  
            WHEN @sortColumn = 'ID'  
                THEN ID  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'PhoneNumber'  
                THEN PhoneNumber  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN ''  
            WHEN @sortColumn = 'PhoneNumber'  
                THEN PhoneNumber  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'FaxNumber'  
                THEN FaxNumber  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN ''  
            WHEN @sortColumn = 'FaxNumber'  
                THEN FaxNumber  
            END DESC  
        ,CASE   
            WHEN @sortOrder <> 'ASC'  
                THEN ''  
            WHEN @sortColumn = 'EmailAddress'  
                THEN EmailAddress  
            END ASC  
        ,CASE   
            WHEN @sortOrder <> 'DESC'  
                THEN ''  
            WHEN @sortColumn = 'EmailAddress'  
                THEN EmailAddress  
            END DESC OFFSET @OffsetValue ROWS  
  
    FETCH NEXT @PagingSize ROWS ONLY  
END


--------Dynamic Query  
----DECLARE @sqlQuery VARCHAR(max) = 'SELECT ID,FullName,PhoneNumber,FaxNumber,EmailAddress,count(ID) Over() as FilterTotalCount FROM Employee';  
  
----SET @sqlQuery = @sqlQuery + ' WHERE ((''' + @SearchText + ''' <> '''' AND (FullName LIKE ''%' + @SearchText + '%'' OR PhoneNumber LIKE ''%' + @SearchText + '%'')) OR (''' + @SearchText + ''' = ''''))';  
----SET @sqlQuery = @sqlQuery + ' order by ' + @sortColumn + ' ' + @sortOrder;  
----SET @sqlQuery = @sqlQuery + ' OFFSET ' + cast(@OffsetValue AS VARCHAR(100)) + ' ROWS FETCH NEXT ' + cast(@PagingSize AS VARCHAR(100)) + ' ROWS ONLY';  
  
----EXEC (@sqlQuery);  



--public DataTable GetData(string sortColumn,string sortDirection, int OffsetValue, int PagingSize, string searchby) {  
--            DataTable dt = new DataTable();  
--            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString)) {  
--                conn.Open();  
--                SqlCommand com = new SqlCommand("spDataInDataTable", conn);  
--                com.CommandType = CommandType.StoredProcedure;  
--                com.Parameters.AddWithValue("@sortColumn", sortColumn);  
--                com.Parameters.AddWithValue("@sortOrder", sortDirection);  
--                com.Parameters.AddWithValue("@OffsetValue", OffsetValue);  
--                com.Parameters.AddWithValue("@PagingSize", PagingSize);  
--                com.Parameters.AddWithValue("@SearchText", searchby);  
--                SqlDataAdapter da = new SqlDataAdapter(com);  
--                da.Fill(dt);  
--                da.Dispose();  
--                conn.Close();  
--            }  
--            return dt;  
--        }
