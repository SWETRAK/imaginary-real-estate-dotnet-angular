import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreTopBarComponent } from './ire-topbar.component';

describe('IreTopbarComponent', () => {
  let component: IreTopBarComponent;
  let fixture: ComponentFixture<IreTopBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreTopBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreTopBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
