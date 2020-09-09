SELECT TOP 5 s.id, s.name, s.daily_rate, s.max_occupancy, s.is_accessible FROM space s JOIN venue v ON s.venue_id = v.id WHERE NOT EXISTS(SELECT* FROM reservation WHERE reservation.space_id = s.id AND ('06/20/2020' > end_date OR '06/24/2020'<start_date))
                                              AND venue_id = 1 GROUP BY s.name, s.id, s.daily_rate, s.max_occupancy, s.is_accessible ORDER BY daily_rate;

											   SELECT ISNULL(NULL, 1)
											   SELECT * FROM reservation RIGHT OUTER JOIN space ON reservation.space_id = space.id WHERE space.venue_id = 10
											   SELECT id, venue_id, name, is_accessible, ISNULL(open_from, 1) open_from, ISNULL(open_to, 12) open_to, daily_rate, max_occupancy FROM space

											   SELECT TOP 5 s.id, s.name, s.daily_rate, s.max_occupancy, s.is_accessible, ISNULL(open_from, 1) open_from, ISNULL(open_to, 12) open_to FROM space s JOIN venue v ON s.venue_id = v.id 
											   WHERE NOT EXISTS(SELECT * FROM reservation WHERE reservation.space_id = s.id) AND ((8 >= open_from AND 8 <= open_to) OR open_from IS NULL) AND 10 <= max_occupancy AND venue_id = 1 
											   
											   Union
											   SELECT s.id, s.name, s.daily_rate, s.max_occupancy, s.is_accessible, ISNULL(open_from, 1) open_from, ISNULL(open_to, 12) open_to FROM space s 
											   JOIN venue v ON s.venue_id = v.id WHERE EXISTS (SELECT * FROM reservation WHERE reservation.space_id = s.id AND (('6/20/2020' NOT BETWEEN reservation.start_date and reservation.end_date) AND ('6/24/2020' NOT BETWEEN reservation.start_date and reservation.end_date)) AND ((8 >= open_from AND 8 <= open_to) OR open_from IS NULL)
											    AND 10 <= max_occupancy AND venue_id = 1)
											ORDER BY s.name DESC
											    
												
												   
												 



											   SELECT TOP 5 * FROM space s
											   WHERE venue_id = 1

											   AND s.id NOT IN (SELECT s.id from reservation r JOIN space s on r.space_id = s.id
											   WHERE s.venue_id = 1 AND r.end_date >= '6/20/2020' AND r.start_date <= '6/24/2020')
											 
											    

											   


