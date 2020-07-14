CREATE TABLE Aircraft
(
    Fleet           CHAR(5) PRIMARY KEY,
    Aircaft_Type    VARCHAR(20),
    Capacity        INT,
    created_at      TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at      TIMESTAMPTZ,
    deleted_at      TIMESTAMPTZ
);

CREATE TABLE Flight_Data
(
    Flight_No       CHAR(5) PRIMARY KEY,
    Fleet           CHAR(5) REFERENCES Aircraft(Fleet),
    Departure_Apt   CHAR(3),
    Arrival_Apt     CHAR(3),
    Dep_Sched       TIMESTAMPTZ,
    Arr_Sched       TIMESTAMPTZ,
    Route_Slot      INT[]
    Price           INT
);

CREATE TABLE Ticket_Header
(
    Booking_code    CHAR(6) UNIQUE,
	entry_id		UUID UNIQUE,
    Passenger_LName VARCHAR(255),
    created_at      TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    updated_at      TIMESTAMPTZ,
	PRIMARY KEY(Booking_code, entry_id)
);

CREATE TABLE Ticket_Detail
(
    Booking_Code    CHAR(6) REFERENCES Ticket_Header(Booking_Code),
	entry_id		UUID REFERENCES Ticket_Header(entry_id),
    Flight_No       CHAR(5) REFERENCES Flight_Data(Flight_No),
    Passenger_FName VARCHAR(255),
    Passenger_LName VARCHAR(255),
    Departure_Date  TIMESTAMPTZ
);