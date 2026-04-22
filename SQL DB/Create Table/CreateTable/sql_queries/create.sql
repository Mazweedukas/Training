CREATE Table Projects (
	project_id SERIAL PRIMARY KEY,
	project_name TEXT NOT NULL,
	date_of_creation DATE NOT NULL DEFAULT CURRENT_DATE,
	status TEXT NOT NULL CHECK (status in ('open', 'closed')),
	date_of_closure DATE DEFAULT NULL
);

CREATE TABLE Tasks (
    task_id SERIAL PRIMARY KEY,
    task_name TEXT NOT NULL,
    project_id INT REFERENCES Projects(project_id),
    assigned_employee_id INT REFERENCES Employees(employee_id),
	task_deadline DATE NOT NULL
);

CREATE TABLE Employees (
	employee_id SERIAL PRIMARY KEY,
	employee_name TEXT NOT NULL,
	employee_surname TEXT NOT NULL,
	date_of_birth DATE NOT NULL,
	education TEXT DEFAULT NULL
);

CREATE TABLE Task_status_history (
    id SERIAL PRIMARY KEY,
    task_id INT NOT NULL REFERENCES Tasks(task_id),
    status TEXT NOT NULL CHECK (status IN ('open', 'completed', 'requires revision', 'accepted')),
    changed_at DATE NOT NULL DEFAULT CURRENT_DATE,
    changed_by INT NOT NULL REFERENCES Employees(employee_id)
);

CREATE TABLE Employee_projects (
	id SERIAL PRIMARY KEY,
	employee_id INT NOT NULL REFERENCES employees(employee_id),
	project_id INT NOT NULL REFERENCES projects(project_id)
);

CREATE TABLE Positions (
	position_id SERIAL PRIMARY KEY,
	position_name TEXT NOT NULL UNIQUE
);

CREATE TABLE Employee_project_roles (
	id SERIAL PRIMARY KEY,
	employee_id INT NOT NULL,
	project_id INT NOT NULL,
	position_id INT NOT NULL,
	FOREIGN KEY (employee_id) REFERENCES Employees(employee_id),
	FOREIGN KEY (project_id) REFERENCES Projects(project_id),
	FOREIGN KEY (position_id) REFERENCES Positions(position_id)
);