/* ****************************************************************************
 * File name: WebApp.sql
 * 
 * Author: Tamás Kiss
 * Created: Oct/25/2024
 * 
 * Last Editor: Tamás Kiss
 * Last Modified: Nov/6/2024
 * 
 * Copyright (C) Deutsche Telekom, 2024.
 * ************************************************************************* */

/*
if exists
(
    select 1 
    from information_schema.tables 
    where table_type = 'BASE TABLE' and table_name = 'WebApp'
)
begin
    drop table WebApp;
end
*/

create table WebApp (
    Id int primary key,
    [Text] nvarchar(100)
);

/*
insert into WebApp (Id, Text) values (1, 'Text-1');
insert into WebApp (Id, Text) values (2, 'Text-2');
insert into WebApp (Id, Text) values (3, 'Text-3');
*/

select * from WebApp;