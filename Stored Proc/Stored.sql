--1
-- generate random exam 

ALTER procedure [dbo].[rand_ques] ( @crs_name varchar(50),@mcq_no int, @tf_no int ) as
begin

select * from
(select top(@mcq_no)  ques_header,ques_choice1,ques_choice2 , ques_choice3 , ques_choice4 , q.ques_id
from Question q
join ques_choices ch
on q.ques_id = ch.ques_id 
join Course c
on c.crs_id = q.crs_id
where crs_name = @crs_name
and ques_type = 'mcq'
order by NEWID() ) t1
union 
select * from 
(select top(@tf_no) ques_header,ques_choice1,ques_choice2 , ques_choice3 , ques_choice4 , q.ques_id
from Question q
join ques_choices ch
on q.ques_id = ch.ques_id 
join Course c
on c.crs_id = q.crs_id
where crs_name = @crs_name
and ques_type = 'T/F'
order by NEWID() ) t2
end

--2
-- get question header and choices  
ALTER proc [dbo].[get_ques_choices] @ques_id int 
as 

select ques_header ,
ques_choice1 , ques_choice2 , ques_choice3 , ques_choice4 
from question
join ques_choices
on question.ques_id = ques_choices.ques_id
where question.ques_id = @ques_id



--3
-- save question with student answer 

ALTER proc [dbo].[saving_ques] @ques_id int , @answer nvarchar(120) 
as 
begin
declare @email varchar(50)
declare @model_ans nvarchar(120)
declare @exam_id int ,
		@st_id int 


set @exam_id = (Select top 1 exam_id from exam order by exam_id desc);
set @model_ans = (select ques_answer from question where ques_id= @ques_id )
set @email = (select top 1 login_user from login_student order by id desc)
set @st_id = (select st_id from Student where user_login = @email)

insert into exam_taken (st_id,exam_id,ques_id,answer) values
							(@st_id,
							@exam_id,
							@ques_id,
							(@answer))

update exam_taken
set answer_correct = 1
from exam 
join exam_taken 
on exam.exam_id = exam_taken.exam_id
join question 
on question.ques_id = exam_taken.ques_id
where exam.exam_id = @exam_id and question.ques_id =@ques_id
and answer = ques_answer 

update exam_taken
set answer_correct = 0
from exam 
join exam_taken 
on exam.exam_id = exam_taken.exam_id
join question 
on question.ques_id = exam_taken.ques_id
where exam.exam_id = @exam_id and question.ques_id =@ques_id
and answer != ques_answer


end

--4
-- correct exam 

ALTER proc [dbo].[exam_final_score]  @st_mark int output 
as 
begin

declare @exam_id int 
set @exam_id = ( select top(1) exam_id  from exam order by exam_id desc ) 

declare @final_result int 
set @final_result = ( select sum( answer_correct ) from exam_taken where exam_id = @exam_id ) 

update exam
set st_mark = @final_result 
where exam_id = @exam_id


select @st_mark = st_mark
from exam
where exam_id = @exam_id
end





--SSRS REPORTS


--1
-- Student info by dept id
ALTER procedure [dbo].[get_student_info] (@dep int)
as 
select * from student
where dept_id = @dep 


--2
-- Student grades by student id 

ALTER proc [dbo].[get_student_grades](@st_id int )
as
begin 
SELECT  student.st_fname, course.crs_name, max(exam.st_mark) as st_mark , exam.exam_id
FROM    exam 
JOIN exam_taken 
on exam_taken.exam_id = exam.exam_id
join course 
ON exam.crs_id = course.crs_id INNER JOIN
student ON student.st_id = exam_taken.st_id
WHERE        exam_taken.st_id =@st_id
group by exam.exam_id , st_fname , course.crs_name
end 


--3
-- Student exam answer by student id , exam id 
ALTER proc [dbo].[get_student_exam_answer] (@st_id int, @ex_id int )
as
begin 

select exam_id ,  ques_header ,  ques_choice1 , ques_choice2 , ques_choice3 , ques_choice4 , answer , ques_answer , answer_correct
from exam_taken
join question
on exam_taken.ques_id = question.ques_id
join ques_choices 
on ques_choices.ques_id = question.ques_id
where st_id =@st_id and exam_id =@ex_id
end 

--4
-- exam question by exam id 

ALTER proc [dbo].[get_exam_ques] (@ex_id int )
as
begin 

select exam_id ,  ques_header ,  ques_choice1 , ques_choice2 , ques_choice3 , ques_choice4
from exam_taken
join question
on exam_taken.ques_id = question.ques_id
join ques_choices
on ques_choices.ques_id = question.ques_id
where exam_id = @ex_id
end 


--5
-- instructor courses , student count by instructor id 

ALTER proc [dbo].[get_instructor_course](@ins_id int )
as
begin 
select ins_id,crs_name ,  COUNT(st_id) as student_number
from ins_course
join course 
on ins_course.crs_id= course.crs_id
join st_course
on st_course.crs_id = course.crs_id
where ins_id = @ins_id
group by ins_id , crs_name
end 

--6
-- course topics by course id 

ALTER proc [dbo].[get_course_topics] (@course_id int )
as
begin 
SELECT        course.crs_id, course.crs_name, topic.topic_name, course.crs_dur
FROM            course INNER JOIN
                         topic ON course.crs_id = topic.crs_id
WHERE        (topic.crs_id = @course_id)

end 