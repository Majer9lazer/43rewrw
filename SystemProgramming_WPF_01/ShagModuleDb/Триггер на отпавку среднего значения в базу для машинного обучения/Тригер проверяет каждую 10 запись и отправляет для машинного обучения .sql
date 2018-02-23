   create trigger GetTensRow
   on Processes after INSERT
   as
   DECLARE @AVERAGEmem int
   BEGIN
	if((select i.Id from inserted i)%10=0)
		BEGIN
		print 'Десятый элемент'
			Select @AVERAGEmem=AVG(p.ProcessMemorySize) from Processes p
			insert into DataScienceTable
			values(@AVERAGEmem)
		END
	else
		BEGIN	
			print 'Это не десятый элемент таблицы'
		END
   END
