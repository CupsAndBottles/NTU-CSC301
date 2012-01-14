;
; identify start station
; get lists branching out from that station (settle each independently as there is no way that routes will pass through the start.)
; for each of the >=2 lists (could be 1 empty list),
;   keep going till hit either of 1) interchange station OR 2) end point OR 3) original Start station <end of line will terminate implicitily)
;   each station traversed will be appended to the resultRoute list
;   if (1) run method on interchange station, pass in resultRoute
;   if (2) print out resultRoute
;   if (3) terminate


;; TODO
;; convert 3-4 digit station code to friendly name



; Declaration of Lines

(define (northEastLine)
  (list 
   "HarbourFront"
   "Outram Park"
   "Chinatown"
   "Clarke Quay"
   "Dhoby Ghaut"
   "Little India"
   "Farrer Park"
   "Boon Keng"
   "Potong Pasir"
   "Woodleigh"
   "Serangoon"
   "Kovan"
   "Hougang"
   "Buangkok"
   "Sengkang"
   "Punggol"
   ))


(define (eastWestLine)
  (list 
   "Pasir Ris"
   "Tampines"
   "Simei"
   "Tanah Merah"
   "Bedok"
   "Kembangan"
   "Eunos"
   "Paya Lebar"
   "Aljunied"
   "Kallang"
   "Lavender"
   "Bugis"
   "City Hall"
   "Raffles Place"
   "Tanjong Pagar"
   "Outram Park"
   "Tiong Bahru"
   "Redhill"
   "Queenstown"
   "Commonwealth"
   "Buona Vista"
   "Dover"
   "Clementi"
   "Jurong East"
   "Chinese Garden"
   "Lakeside"
   "Boon Lay"
   "Pioneer"
   "Joo Koon"
   ))


(define (northSouthLine)
  (list 
   "Jurong East"
   "Bukit Batok"
   "Bukit Gombak"
   "Chua Chu Kang"
   "Yew Tee"
   "Kranji"
   "Marsiling"
   "Woodlands"
   "Admiralty"
   "Sembawang"
   "Yishun"
   "Khatib"
   "Yio Chu Kang"
   "Ang Mo Kio"
   "Bishan"
   "Braddell"
   "Toa Payoh"
   "Novena"
   "Newton"
   "Orchard"
   "Somerset"
   "Dhoby Ghaut"
   "City Hall"
   "Raffles Place"
   "Marina Bay"
   ))


(define (circleLine)
  (list
   "Dhoby Ghaut"
   "Bras Basah"
   "Esplanade"
   "Promenade"
   "Nicoll Highway"
   "Stadium"
   "Mountbatten"
   "Dakota"
   "Paya Lebar"
   "MacPherson"
   "Tai Seng"
   "Bartley"
   "Serangoon"
   "Lorong Chuan"
   "Bishan"
   "Marymount"
   "Caldecott"
   "Botanic Gardens"
   "Farrer Road"
   "Holland Village"
   "Buona Vista"
   "one-north"
   "Kent Ridge"
   "Haw Par Villa"
   "Pasir Panjang"
   "Labrador Park"
   "Telok Blangah"
   "HarbourFront"
   )
  )

( define (changiLine)
   (list 
    "Tanah Merah"
    "Expo"
    "Changi Airport"
    )
   )



(define (interchangeStation)
  (list 
   "Dhoby Ghaut"
   "Paya Lebar"
   "Serangoon"
   "Bishan"
   "Buona Vista"
   "HarbourFront"
   "Tanah Merah"
   "City Hall"
   "Raffles Place"
   "Outram Park"
   "Jurong East"
   ))

; Aux Functions


; Determine if station is a valid station
(define (isStation stationName)
  (or ( isEWL stationName) ( isNEL stationName) ( isNSL stationName ) ( isCGL stationName) ( isCCL stationName))
  )

; Determine if station is an interchange
(define (isInterchange stationName)
  (list? (member stationName (interchangeStation) ))
  )

; Check for various lines
(define (isNEL . args)
  (let ((isAllNEL #t))
    (for-each
     (lambda (x) 
       (if (not (list? (member x (northEastLine) )))
           (set! isAllNEL #f)
           )
       )
     args)
    isAllNEL
    )
  )


(define (isEWL . args)
  (let ((isAllEWL #t))
    (for-each
     (lambda (x) 
       (if (not (list? (member x (eastWestLine) )))
           (set! isAllEWL #f)
           )
       )
     args)
    isAllEWL
    )
  )

(define (isNSL . args)
  (let ((isAllNSL #t))
    (for-each
     (lambda (x) 
       (if (not (list? (member x (northSouthLine) )))
           (set! isAllNSL #f)
           )
       )
     args)
    isAllNSL
    )
  )

(define (isCCL . args)
  (let ((isAllCCL #t))
    (for-each
     (lambda (x) 
       (if (not (list? (member x (circleLine) )))
           (set! isAllCCL #f)
           )
       )
     args)
    isAllCCL
    )
  )

(define (isCGL . args)
  (let ((isAllCGL #t))
    (for-each
     (lambda (x) 
       (if (not (list? (member x (changiLine) )))
           (set! isAllCGL #f)
           )
       )
     args)
    isAllCGL
    )
  )

; returns true if stn1, stn2 and stn3 are all on the same line
(define (isSameLine stn1 stn2 stn3)
  (or
   (isNEL stn1 stn2 stn3)
   (isEWL stn1 stn2 stn3)
   (isNSL stn1 stn2 stn3)
   (isCCL stn1 stn2 stn3)
   (isCGL stn1 stn2 stn3)
   )
  )



; Retrives a list of list containing the stations in order branching
; out from the given location, except those with either previousLoc or 
; nextLoc as the first station in the result list.
(define returnForwardBackword
  (lambda (stationName previousLoc nextLoc)
    (let ((condensedResult (list )))
      
      (begin
        (if 
         (and 
          (isNEL stationName) 
          (> (length (member stationName (northEastLine))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (northEastLine)))))) 
          (not (equal? nextLoc (car (cdr (member stationName (northEastLine))))))
          )
         (set! condensedResult (append condensedResult (list( cdr (member stationName (northEastLine) )))))
         )
        
        (if 
         (and 
          (isNEL stationName) 
          (> (length (member stationName (reverse (northEastLine)))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (reverse (northEastLine))))))) 
          (not (equal? nextLoc (car (cdr (member stationName (reverse (northEastLine)))))))
          ) 
         (set! condensedResult (append condensedResult (list(cdr (member stationName (reverse (northEastLine)) )))))
         )
        
        (if 
         (and 
          (isEWL stationName) 
          (> (length (member stationName (eastWestLine))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (eastWestLine))))))
          (not (equal? nextLoc (car (cdr (member stationName (eastWestLine))))))
          ) 
         (set! condensedResult (append condensedResult (list(cdr (member stationName (eastWestLine) )))))
         )
        
        (if 
         (and 
          (isEWL stationName) 
          (> (length (member stationName (reverse (eastWestLine)))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (reverse (eastWestLine)))))))
          (not (equal? nextLoc (car (cdr (member stationName (reverse (eastWestLine)))))))
          ) 
         (set! condensedResult (append condensedResult (list(cdr (member stationName (reverse (eastWestLine)) )))))
         )
        
        (if 
         (and 
          (isNSL stationName) 
          (> (length (member stationName (northSouthLine))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (northSouthLine))))))
          (not (equal? nextLoc (car (cdr (member stationName (northSouthLine))))))
          ) 
         (set! condensedResult (append condensedResult (list(cdr (member stationName (northSouthLine) )))))
         )
        
        (if 
         (and 
          (isNSL stationName) 
          (> (length (member stationName (reverse (northSouthLine)))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (reverse (northSouthLine)))))))
          (not (equal? nextLoc (car (cdr (member stationName (reverse (northSouthLine)))))))
          )
         (set! condensedResult (append condensedResult (list(cdr (member stationName (reverse (northSouthLine)) )))))
         )
        
        (if 
         (and 
          (isCCL stationName) 
          (> (length (member stationName (circleLine))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (circleLine))))))
          (not (equal? nextLoc (car (cdr (member stationName (circleLine))))))
          ) 
         (set! condensedResult (append condensedResult (list(cdr (member stationName (circleLine) )))))
         )
        
        (if 
         (and 
          (isCCL stationName) 
          (> (length (member stationName (reverse (circleLine)))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (reverse (circleLine)))))))
          (not (equal? nextLoc (car (cdr (member stationName (reverse (circleLine)))))))
          )
         (set! condensedResult (append condensedResult (list(cdr (member stationName (reverse (circleLine)) )))))
         )
        
        (if 
         (and 
          (isCGL stationName) 
          (> (length (member stationName (changiLine))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (changiLine))))))
          (not (equal? nextLoc (car (cdr (member stationName (changiLine))))))
          ) 
         (set! condensedResult (append condensedResult (list(cdr (member stationName (changiLine) )))))
         )
        
        (if 
         (and 
          (isCGL stationName) 
          (> (length (member stationName (reverse (changiLine)))) 1 ) 
          (not (equal? previousLoc (car (cdr (member stationName (reverse (changiLine)))))))
          (not (equal? nextLoc (car (cdr (member stationName (reverse (changiLine)))))))
          )
         (set! condensedResult (append condensedResult (list(cdr (member stationName (reverse (changiLine)) )))))
         )
        
        condensedResult
        )
      
      
      )
    )
  )

(define allPossiblePaths '())


(define (printPossiblePath  resultListItem)
  (let ((numberOfStation (car resultListItem)) (stationList (cadr resultListItem)) (interchangeStations (caddr resultListItem)))
    (newline)
    (display "Full Route : ") (newline)
    (display (car stationList)) (printPathOfStations (cdr stationList) (car stationList)) (newline)
    (display "Condensed Route : ") (newline)

    (printInitialPath stationList "") (printPathOfStationsCondensed (cdr stationList) (car stationList) (getCommonLine (car stationList) (car (cdr stationList))))(newline)
    (display "Number of stations : ") (display numberOfStation) (newline) 
    (display "Number of interchanges : ") (display (- (length interchangeStations) 1 ) ) (newline) 
    (newline)
    )
  )


; Returns the Line Name that the 2 interchanges are common on
(define (getCommonLine stationOne stationTwo)
  (let ((commonLine ""))

  (cond
    ((isNEL stationOne stationTwo)
     (set! commonLine "NE")
    )
    ((isEWL stationOne stationTwo)
     (set! commonLine "EW")
    )
    ((isNSL stationOne stationTwo)
     (set! commonLine "NS")
    )
    ((isCCL stationOne stationTwo)
     (set! commonLine "CC")
     )
    ((isCGL stationOne stationTwo)
     (set! commonLine "EW")
     )
    )
    commonLine
    )
)

(define (printInitialPath stationList lastStation)

  (display (car stationList)) (display " -> ")
  (display (getCommonLine (car stationList) (car (cdr stationList))) )
  (display " -> ")

)

(define (printPathOfStations stationList lastStation)
  (cond
    ((null? stationList)
     )
    ( else 
      (begin
        (display " -> ")
        (cond
          ( (= 1 (length stationList) )
            (display (car stationList))
            )
          (else (display (car stationList)) )
          )
        (printPathOfStations (cdr stationList) (car stationList))
        )
      )
    )
  )


(define (printPathOfStationsCondensed stationList lastStation lastLine)
  (cond
    ((= (length stationList) 1)
      (display (car stationList))
     )
    ((and (isInterchange (car stationList))  (not (equal? lastLine (getCommonLine (car stationList) (car (cdr stationList)) ))) ) 
      (begin 
        (printInitialPath stationList lastStation)
        (set! lastLine (getCommonLine (car stationList) (car (cdr stationList)) ) )
        (printPathOfStationsCondensed (cdr stationList) (car stationList) lastLine)
      ) 
     )
    ( else ; Not an interchange or there is no change in the line
     (printPathOfStationsCondensed (cdr stationList) (car stationList) lastLine)
      )
    )
  )

; Main Functions

(define terminateThisFlow #f)
(define iCounter 0)



(define (exec)
  (let ((startLoc "") (endLoc "")) 
  (begin
    (display "Start Station: ")
    (set! startLoc (read))
    (display "End Station: ")
    (set! endLoc (read))
    (findPath startLoc endLoc)
    )
  )
  )


(define findPath
  (let ((pathsFound '()))
    (lambda (startLoc endLoc)
      (begin 
        
        ; Validation
        (cond ( ( or (null? startLoc) (not (isStation startLoc))) (display "Invalid Start Station") )
              ( ( or (null? endLoc) (not (isStation endLoc))) (display "Invalid End Station") )
              ( (equal? startLoc endLoc) (display "Start Station and End Station cannot be the same!"))
              (else 
               (begin
        
                 (set! allPossiblePaths '())

                 (recursiveSearch startLoc endLoc (list startLoc) 0 startLoc startLoc (list startLoc) '())
                 (showShortestX allPossiblePaths 3)
                 
                 )              
               )
              )
        )
      )
    )
  )

  
; Main recursive function that discovers paths
(define (recursiveSearch startLoc endLoc resultList numberOfStation previousLoc nextLoc interchangeVisited collectionOfRoutes)
  
  (begin 
    
    (let ((searchResult (returnForwardBackword startLoc previousLoc nextLoc))) 
      
      ( for-each (lambda(x)
                   
                   (let ((interchangeVisitedLocal interchangeVisited) (resultListLocal resultList) (numberOfStationLocal numberOfStation) (previousStation previousLoc) (nextStation "") ) 
                     (begin
                       
                       (let ((iCounter (length x)))
                         
                         
                         ; Loop inner route
                         (do (; init
                              (i 0 (+ i 1))
                              )
                           ( ; test condition
                            (or (= i iCounter)  terminateThisFlow (<= (length x) 0 ))
                            )
                           
                           ; locals to keep track of progress
                           (set! resultListLocal (append  resultListLocal (list (car x)) ))
                           (set! numberOfStationLocal (+ 1 numberOfStationLocal))                           
                           (if (not (null? (cdr x)))
                               (set! nextStation (car (cdr x)))
                               )
                           
                           
                           ;testing for termination and branch conditions
                           (cond 
                             ((equal? (car x) endLoc) 
                              (begin 
                                (set! terminateThisFlow #t)
                                (set! allPossiblePaths (append allPossiblePaths (list (list numberOfStationLocal resultListLocal  interchangeVisitedLocal ) )))
                                )
                              )
                             ((equal? (car x) startLoc) 
                              (set! terminateThisFlow #t)
                              )
                             ((not (eq? #f (member (car x) interchangeVisitedLocal))) 
                              (set! terminateThisFlow #t)
                              )
                             ((and (isInterchange (car x) ) (eq? #f terminateThisFlow) )
                              (begin 

                                (recursiveSearch (car x) endLoc resultListLocal numberOfStationLocal previousStation nextStation (append  interchangeVisitedLocal (list (car x)) )  collectionOfRoutes )
                                
                                )
                              )
                             )
                           
                           (set! previousStation (car x))
                           (set! x (cdr x) )
                           )
                         
                         (set! terminateThisFlow #f) ; flag
                         
                         )
                       )                  
                     
                     )
                   )
                
                 searchResult
                 )      
      (set! iCounter 0)      ; reset counter
      )
    
    )
  
  collectionOfRoutes
  
  )


; Utils


; For Selection of 3 shortest routes
(define (showShortestX list number)
  (cond
    ((null? list)
     )
    ((> number 0)
     (begin
       (printPossiblePath (smallest list (car list)))
       (showShortestX (remove list (smallest list (car list))) (- number 1))
       )
     )
    )
  )

(define (remove L A)                ; remove the first occurance of atom A from L
  (cond ( (null? L) '() )           
        ;( (= (getNumberOfStations (car L)) (getNumberOfStations A)) (cdr L))    ; Match found! 
        ( (equal? (car L) A) (cdr L))    ; Match found! 
        (else (cons (car L)(remove (cdr L) A)))   ; keep searching
        )
  )

(define (smallest L A)             ; looks for the smallest element in the list
  ; atom A is the current smallest
  (cond ( (null? L) A)
        ( (< (getNumberOfStations (car L)) (getNumberOfStations A)) (smallest (cdr L)(car L)))
        (else (smallest (cdr L) A ))
        )
  )

(define (getNumberOfStations x)
  (car x)
  )

; Removed and clean duplicates from the list
(define (removeDup ls)
  (if (null? ls)
      '()
      (let ((first (car ls)))
        (let loop ((known first)
                   (rest (cdr ls))
                   (so-far (list first)))
          (if (null? rest)
              (reverse so-far)
              (let ((first-remaining (car rest)))
                (loop first-remaining
                      (cdr rest)
                      (if (equal? known first-remaining)
                          so-far
                          (cons first-remaining so-far)))))))))