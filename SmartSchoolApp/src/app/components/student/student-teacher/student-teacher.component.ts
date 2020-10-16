import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Util } from 'src/app/helpers/Util';
import { Discipline } from 'src/app/models/Discipline';
import { Teacher } from 'src/app/models/Teacher';

@Component({
  selector: 'app-student-teacher',
  templateUrl: './student-teacher.component.html',
  styleUrls: ['./student-teacher.component.css']
})
export class StudentTeacherComponent implements OnInit {
  @Input() public teachers: Teacher[];
  @Output() closeModal = new EventEmitter();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  concatDisciplines(disciplines: Discipline[]): string {
    return Util.concatArray(disciplines);
  }

  selectTeacher(teacher: Teacher): void {
    this.closeModal.emit(null);
    this.router.navigate(['/teacher', teacher.id]);
  }
}
