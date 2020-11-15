import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Util } from 'src/app/helpers/Util';
import { Student } from 'src/app/models/Student';
import { Teacher } from 'src/app/models/Teacher';
import { UpdateTeacher } from 'src/app/models/UpdateTeacher';
import { StudentService } from 'src/app/services/Student.service';
import { TeacherService } from 'src/app/services/Teacher.service';

@Component({
  selector: 'app-teacher-detail',
  templateUrl: './teacher-detail.component.html',
  styleUrls: ['./teacher-detail.component.scss']
})
export class TeacherDetailComponent implements OnInit, OnDestroy {
  private unsubscriber = new Subject();

  public teacherForm: FormGroup;
  public modalRef: BsModalRef;
  public title = 'Detalhes do professor';
  public teacher: Teacher;
  public updateTeacher: UpdateTeacher;
  public teacherSelected: Teacher;
  public students: Student[];

  constructor(private router: Router, private route: ActivatedRoute, private modalService: BsModalService,
              private teacherService: TeacherService, private studentService: StudentService,
              private spinner: NgxSpinnerService, private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.createForm();
    this.getTeachers();
  }

  createForm(): void {
    this.teacherForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required]
    });
  }

  getTeachers(): void {
    this.spinner.show();

    const teacherId = +this.route.snapshot.paramMap.get('id');

    this.teacherService.getById(teacherId)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((teacher: Teacher) => {
        this.teacherSelected = teacher;
        this.teacherForm.patchValue(teacher);
      },
      (errorResp: any) => {
        this.toastr.error(errorResp.error);
        console.error(errorResp);
        this.spinner.hide();
      },
      () => this.spinner.hide());
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

  saveTeacher(): void {
    this.spinner.show();

    this.updateTeacher = {...this.teacherForm.value};

    const fullName = this.updateTeacher.nome;
    this.updateTeacher.nome = Util.getFirstName(fullName);
    this.updateTeacher.sobrenome = Util.getLastName(fullName);

    this.teacherService.patch(this.updateTeacher)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((teacher: UpdateTeacher) => {
        this.toastr.success('Professor atualizado com sucesso.');
        this.router.navigate(['/teacher', teacher.id]);
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
