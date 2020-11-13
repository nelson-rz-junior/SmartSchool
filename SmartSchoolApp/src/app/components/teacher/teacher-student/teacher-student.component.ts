import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/app/models/Student';

@Component({
  selector: 'app-teacher-student',
  templateUrl: './teacher-student.component.html',
  styleUrls: ['./teacher-student.component.scss']
})
export class TeacherStudentComponent implements OnInit {
  @Input() public students: Student[];
  @Output() public closeModal = new EventEmitter();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  selectStudent(id: number): void {
    this.closeModal.emit(null);
    this.router.navigate(['/student', id]);
  }
}
