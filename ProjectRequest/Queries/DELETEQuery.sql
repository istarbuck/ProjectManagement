DELETE 
FROM Request
WHERE reuqestID = 1646

DELETE 
FROM Answer
WHERE requestID = 1464

DELETE 
FROM Attachment
WHERE requestID = 1464

DELETE
FROM TaskList
WHERE requestID = 1464

DELETE 
FROM RequestAssignment
WHERE requestID = 1464
