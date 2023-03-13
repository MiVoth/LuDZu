select per.firstname || ' ' || per.lastname Verkündiger,
	per.Gender Geschlecht,
	partner.firstname || ' ' || partner.lastname Partner,
	sched.talk_id ArtId,
	sched.theme Art,
	sched.source Quelle,
	sched.study_number Lektion,
    per.firstname Vorname,
    ass.date Datum,
	(select count(1) from exceptions exc where strftime('%W',ass.date) == strftime('%W',exc.date) and strftime('%Y',ass.date) == strftime('%Y',exc.date)) > 0 Dienstwoche
	-- (select Count(1) from exceptions exc where (exc.date > '2022-09-01') and (exc.date2 < '2022-10-01') and (ass.date > exc.date) and (ass.date < exc.date2) and exc.active = 1) > 0 Dienstwoche,
	-- ass.* 
	from lmm_assignment ass 
left outer join persons per on per.id == ass.assignee_id
left outer join persons partner on partner.id == ass.assistant_id
left outer join lmm_schedule sched on sched.id == ass.lmm_schedule_id
where ass.date > @from and ass.date < @to and sched.study_number > 0
and ass.completed=0
order by ass.date, sched.talk_id