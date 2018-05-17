#!/usr/bin/python3

from reportsmodule import *

SQLCommand = """SELECT UserName, EventId, EventName, Amount, FORMAT(Date,'yyyy-MM'), FORMAT(Month,'yyyy-MM') FROM CostsModels WHERE FORMAT(Month,'yyyy-MM') = '%s' ORDER BY Date"""

SQLCommand1 = """SELECT UserName, Member, Amount, FORMAT(Date,'yyyy-MM'), FORMAT(Month,'yyyy-MM') FROM PaymentsModels WHERE FORMAT(Month,'yyyy-MM') = '%s' ORDER BY Date"""

if len(sys.argv) > 1:
    target_month = sys.argv[1]
else:
    target_month = ""

print (str(sys.argv))

cursor.execute(SQLCommand % target_month) 

results = cursor.fetchone() 

workbook = xlsxwriter.Workbook('C:\\Users\\Sergiu\\source\\repos\\ArtClubProject\\ArtClub\\ArtClub\\ReportingFolder\\Income_Rerpot.xlsx')

worksheet = workbook.add_worksheet('Expenses')

# Set column width.
worksheet.set_column(0, 0, 12)
worksheet.set_column(1, 1, 12)
worksheet.set_column(2, 2, 12)
worksheet.set_column(3, 4, 12)
worksheet.set_column(4, 4, 12)
worksheet.set_column(5, 5, 12)

# Format
f_text_center_tahoma_8 = workbook.add_format(text_center_tahoma_8)
f_headers_format = workbook.add_format(headers_format)
f_text_left_tahoma_8 = workbook.add_format(text_left_tahoma_8)

# Header list.
headers = ['User', 'Event Id', 'Event Name', 'Amount', 'Date', 'Month']

# Writing header.
writeHeader(worksheet, 0, 0, headers, format=f_headers_format)

row = 1

while results:
    worksheet.write(row, 0, str(results[0]), f_text_center_tahoma_8)
    worksheet.write(row, 1, str(results[1]), f_text_center_tahoma_8)
    worksheet.write(row, 2, str(results[2]), f_text_center_tahoma_8)
    worksheet.write(row, 3, str(results[3]), f_text_left_tahoma_8)
    worksheet.write(row, 4, str(results[4]), f_text_center_tahoma_8)
    worksheet.write(row, 5, str(results[5]), f_text_center_tahoma_8)

    row += 1

    results = cursor.fetchone() 

#-------------------------------------------------------------

cursor.execute(SQLCommand1 % target_month) 

results = cursor.fetchone() 

worksheet1 = workbook.add_worksheet('Income')

# Set column width.
worksheet1.set_column(0, 0, 12)
worksheet1.set_column(1, 1, 12)
worksheet1.set_column(2, 2, 12)
worksheet1.set_column(3, 4, 12)
worksheet1.set_column(4, 4, 12)

# Format
f_text_center_tahoma_8 = workbook.add_format(text_center_tahoma_8)
f_headers_format = workbook.add_format(headers_format)
f_text_left_tahoma_8 = workbook.add_format(text_left_tahoma_8)

# Header list.
headers = ['User', 'Member', 'Amount', 'Date', 'Month']

# Writing header.
writeHeader(worksheet1, 0, 0, headers, format=f_headers_format)

row = 1

while results:
    worksheet1.write(row, 0, str(results[0]), f_text_center_tahoma_8)
    worksheet1.write(row, 1, str(results[1]), f_text_center_tahoma_8)
    worksheet1.write(row, 2, str(results[2]), f_text_left_tahoma_8)
    worksheet1.write(row, 3, str(results[3]), f_text_center_tahoma_8)
    worksheet1.write(row, 4, str(results[4]), f_text_center_tahoma_8)

    row += 1

    results = cursor.fetchone() 

connection.close()

workbook.close()