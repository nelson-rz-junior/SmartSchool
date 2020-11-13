import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Util } from 'src/app/helpers/Util';
import { Student } from 'src/app/models/Student';
import { Teacher } from 'src/app/models/Teacher';
import { StudentService } from 'src/app/services/Student.service';
import { TeacherService } from 'src/app/services/Teacher.service';

@Component({
  selector: 'app-teacher-detail',
  templateUrl: './teacher-detail.component.html',
  styleUrls: ['./teacher-detail.component.scss']
})
export class TeacherDetailComponent implements OnInit, OnDestroy {
  private unsubscriber = new Subject();

  public modalRef: BsModalRef;
  public title = 'Detalhes do professor';
  public teacher: Teacher;
  public teacherSelected: Teacher;
  public students: Student[];

  constructor(private router: Router, private route: ActivatedRoute, private modalService: BsModalService,
              private teacherService: TeacherService, private studentService: StudentService,
              private spinner: NgxSpinnerService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getTeachers();
  }

  getStudents(template: TemplateRef<any>, disciplineId: number): void {
    this.studentService.getByDisciplinaId(disciplineId)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((students: Student[]) => {
        this.students = students;

        this.students.map(s => {
          const fullName = s.nome;
          s.nome = Util.getFirstName(fullName);
          s.sobrenome = Util.getLastName(fullName);
        });

        this.modalRef = this.modalService.show(template);
      },
      (errorResp) => {
        this.toastr.error(errorResp.error);
        console.error(errorResp);
        this.spinner.hide();
      },
      () => this.spinner.hide());
  }

  getTeachers(): void {
    this.spinner.show();

    const teacherId = +this.route.snapshot.paramMap.get('id');

    this.teacherService.getById(teacherId)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((teacher: Teacher) => {
        this.teacherSelected = teacher;
      },
      (errorResp: any) => {
        this.toastr.error(errorResp.error);
        console.error(errorResp);
        this.spinner.hide();
      },
      () => this.spinner.hide());
  }

  goBack(): void {
    this.router.navigate(['/teachers']);
  }

  openModal(template: TemplateRef<any>, disciplineId: number): void {
    this.getStudents(template, disciplineId);
  }

  closeModal(): void {
    this.modalRef.hide();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }
}
