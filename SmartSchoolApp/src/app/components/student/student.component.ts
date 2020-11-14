import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Util } from 'src/app/helpers/Util';
import { Student } from 'src/app/models/Student';
import { Teacher } from 'src/app/models/Teacher';
import { Response } from 'src/app/models/Response';
import { StudentService } from 'src/app/services/Student.service';
import { TeacherService } from 'src/app/services/Teacher.service';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Pagination, PaginationResult } from 'src/app/models/Pagination';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit, OnDestroy
{
  private unsubscriber = new Subject();

  public modalRef: BsModalRef;
  public studentForm: FormGroup;
  public studentSelected: Student;
  public teachers: Teacher[];
  public students: Student[];
  public student: Student;
  public pagination: Pagination;
  public deleteStudentMessage: string;
  public modeSave = 'post';
  public title = 'Alunos';

  constructor(private studentService: StudentService, private route: ActivatedRoute, private teacherService: TeacherService,
              private fb: FormBuilder, private modalService: BsModalService, private toastr: ToastrService,
              private spinner: NgxSpinnerService)
  {
    this.createForm();
  }

  ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3
    } as Pagination;

    this.getStudents();
  }

  selectStudent(student: Student): void {
    this.modeSave = 'patch';
    this.studentSelected = student;

    this.studentForm.patchValue({
      id: student.id,
      nome: Util.getFirstName(student.nome),
      sobrenome: Util.getLastName(student.nome),
      telefone: student.telefone
    });
  }

  unselectStudent(): void {
    this.studentSelected = null;
  }

  createForm(): void {
    this.studentForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      sobrenome: [''],
      telefone: ['', Validators.required]
    });
  }

  getTeachers(template: TemplateRef<any>, id: number): void {
    this.spinner.show();

    this.teacherService.getByStudentId(id)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((teachers: Teacher[]) => {
        this.teachers = teachers;
        this.modalRef = this.modalService.show(template);
      },
      (errorResp: any) => {
        this.toastr.error(errorResp.error);
        console.error(errorResp);
        this.spinner.hide();
      },
      () => this.spinner.hide());
  }

  getStudents(): void {
    const id = +this.route.snapshot.paramMap.get('id');

    this.spinner.show();

    this.studentService.getAll(this.pagination.currentPage, this.pagination.itemsPerPage)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((students: PaginationResult<Student[]>) => {
        this.students = students.result;
        this.pagination = students.pagination;

        if (id > 0) {
          this.selectStudent(this.students.find(s => s.id === id));
        }
      },
      (errorResp: any) => {
        this.toastr.error(errorResp.error);
        console.error(errorResp);
        this.spinner.hide();
      },
      () => this.spinner.hide());
  }

  saveStudent(): void {
    if (this.studentForm.valid) {
      this.spinner.show();

      if (this.modeSave === 'post') {
        this.student = {...this.studentForm.value};
      }
      else {
        this.student = {id: this.studentSelected.id, ativo: this.studentSelected.ativo, ...this.studentForm.value};
      }

      this.studentService[this.modeSave](this.student)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(() => {
          this.getStudents();
          this.toastr.success('Aluno salvo com sucesso!');
        },
        (errorResp: any) => {
          this.toastr.error(errorResp.error);
          console.error(errorResp);
          this.spinner.hide();
        },
        () => this.spinner.hide());
    }
  }

  changeStatus(student: Student): void {
    this.spinner.show();

    student.ativo = !student.ativo;

    this.studentService.changeStatus(student)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((resp: Response) => {
        this.getStudents();
        this.toastr.success(resp.message);
      },
      (errorResp: any) => {
        this.toastr.error(errorResp.error);
        console.error(errorResp);
        this.spinner.hide();
      },
      () => this.spinner.hide());
  }

  pageChanged(event: PageChangedEvent): void {
    this.pagination.currentPage = event.page;
    this.getStudents();
  }

  openModal(template: TemplateRef<any>, studentId: number): void {
    this.getTeachers(template, studentId);
  }

  closeModal(): void {
    this.modalRef.hide();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }
}
